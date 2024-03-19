using Farm.DTOS;
using Farm.Modelss;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;


public class ProduceService : IProduceService
{
    private readonly FarmContext _context;

    public ProduceService(FarmContext context)
    {
        _context = context;
    }

    public void AddProduce(ProduceRequestDTO produceRequest)
    {
        List<string> validationErrors = produceRequest.ValidateInput(IsUpdate: false);
        if (validationErrors.Count > 0)
        {
            throw new System.Exception(string.Join(", ", validationErrors));
        }

        Produce produce = new Produce
        {
            Name = produceRequest.Name,
            Description = produceRequest.Description,
            PlantingDate = produceRequest.PlantingDate.Value,
            ExpectedHarvestDate = produceRequest.ExpectedHarvestDate.Value,
            ActualHarvestDate = produceRequest.ActualHarvestDate.Value,
            GardenId = produceRequest.GardenId.Value,
            Quantity = produceRequest.Quantity.Value,
            Status = produceRequest.Status.Value,  
            Img = produceRequest.Img,
            Price = produceRequest.Price,
        };

        _context.Produces.Add(produce);
        _context.SaveChanges();
    }

	public void DeleteProduce(int id)
	{
		var existingProduce = _context.Produces.Find(id);
		if (existingProduce == null)
		{
			throw new Exception("Produce not found");
		}

		_context.Produces.Remove(existingProduce);
		_context.SaveChanges();
	}

    public List<Produce> GetAllByStatus()
    {
        return _context.Produces.Where(x => x.Status == 3).ToList();
    }

    public List<Produce> GetAllProduces()
    {
        return _context.Produces.ToList();
    }

    public Produce GetProduceById(int id)
    {
        var produce = _context.Produces.Find(id);
        if (produce == null)
        {
            throw new System.Exception("Produce not found");
        }

        return produce;
    }

    public IEnumerable<Produce> SearchProduces(string name)
    {
        IQueryable<Produce> query = _context.Produces;

        if (!string.IsNullOrEmpty(name))
        {
            query = query.Where(p => p.Name.Contains(name));
        }

        return query.ToList();
    }

    public void UpdateProduce(int id, ProduceRequestDTO produceRequest)
    {
        List<string> validationErrors = produceRequest.ValidateInput(IsUpdate: true);
        if (validationErrors.Count > 0)
        {
            throw new System.Exception(string.Join(", ", validationErrors));
        }

        var existingProduce = _context.Produces.Find(id);
        if (existingProduce == null)
        {
            throw new System.Exception("Produce not found");
        }

        existingProduce.Name = produceRequest.Name;
        existingProduce.Description = produceRequest.Description;
        existingProduce.PlantingDate = produceRequest.PlantingDate.Value;
        existingProduce.ExpectedHarvestDate = produceRequest.ExpectedHarvestDate.Value;
        existingProduce.ActualHarvestDate = produceRequest.ActualHarvestDate.Value;
        existingProduce.GardenId = produceRequest.GardenId.Value;
		existingProduce.Quantity = produceRequest.Quantity.Value;
		existingProduce.Status = produceRequest.Status;
        existingProduce.Img = produceRequest.Img;
        existingProduce.Price = produceRequest.Price;


		_context.SaveChanges();
    }
}
