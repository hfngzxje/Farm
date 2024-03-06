using Farm.DTOS;
using Farm.Modelss;

namespace Farm.Service.IService
{
	public interface IProcessService
	{
		List<Process> GetAllProcess();
		Process GetProcessById(int id);
		Process GetProcessByProduceId(int id);
		void AddProcess(ProcessRequestDTO process);
		void UpdateProcess(int id, ProcessRequestDTO process);
		void DeleteProcess(int id);
		IEnumerable<Process> SearchProcess(string name);
	}
}
