using Farm.DTOS;
using Farm.Modelss;

namespace Farm.Service.IService
{
    public interface IGardenService
    {
        List<Garden> GetAllGardens();
        Garden GetGardenById(int id);
        void AddGarden(GardenRequestDTO garden);
        void UpdateGarden(int id, GardenRequestDTO gardenRequest);
        void DeleteGarden(int id);
        IEnumerable<Garden> SearchGardens(string name);
    }
}
