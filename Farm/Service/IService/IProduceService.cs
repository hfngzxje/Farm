using Farm.DTOS;
using Farm.Modelss;
using System;
using System.Collections.Generic;

public interface IProduceService
{
    List<Produce> GetAllProduces();
    Produce GetProduceById(int id);
    void AddProduce(ProduceRequestDTO produce);
    void UpdateProduce(int id, ProduceRequestDTO produceRequest);
    void DeleteProduce(int id);
    IEnumerable<Produce> SearchProduces(string name);
}
