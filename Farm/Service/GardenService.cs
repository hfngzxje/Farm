using Farm.DTOS;
using Farm.Models;
using Farm.Service.IService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;


public class GardenService : IGardenService
{
    private readonly FarmContext _context;

    public GardenService(FarmContext context)
    {
        _context = context;
    }

    public void AddGarden(GardenRequestDTO gardenRequest)
    {
        List<string> validationErrors = gardenRequest.ValidateInput(IsUpdate: false);
        if (validationErrors.Count > 0)
        {
            throw new System.Exception(string.Join(", ", validationErrors));
        }

        Garden garden = new Garden
        {
            Name = gardenRequest.Name,
            Location = gardenRequest.Location,
            Area = gardenRequest.Area,
        };

        _context.Gardens.Add(garden);
        _context.SaveChanges();
    }

    public void DeleteGarden(int id)
    {
        var existingGarden = _context.Gardens.Find(id);
        if (existingGarden == null)
        {
            throw new Exception("Produce not found");
        }

        _context.Gardens.Remove(existingGarden);
        _context.SaveChanges();
    }

    public List<Garden> GetAllGardens()
    {
        return _context.Gardens.ToList();
    }

    public Garden GetGardenById(int id)
    {
        var gardens = _context.Gardens.Find(id);
        if (gardens == null)
        {
            throw new System.Exception("Gardens not found");
        }

        return gardens;
    }


    public IEnumerable<Garden> SearchGardens(string name)
    {
        IQueryable<Garden> query = _context.Gardens;

        if (!string.IsNullOrEmpty(name))
        {   
            query = query.Where(p => p.Name.Contains(name));
        }

        return query.ToList();
    }


    public void UpdateGarden(int id, GardenRequestDTO gardenRequest)
    {
        List<string> validationErrors = gardenRequest.ValidateInput(IsUpdate: true);
        if (validationErrors.Count > 0)
        {
            throw new System.Exception(string.Join(", ", validationErrors));
        }

        var existingGarden = _context.Gardens.Find(id);
        if (existingGarden == null)
        {
            throw new System.Exception("Garden not found");
        }

        existingGarden.Name = gardenRequest.Name;
        existingGarden.Location = gardenRequest.Location;
        existingGarden.Area = gardenRequest.Area;
        _context.SaveChanges();
    }

}
