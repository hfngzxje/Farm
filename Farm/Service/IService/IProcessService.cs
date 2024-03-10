using Farm.DTOS;
using Farm.Modelss;

namespace Farm.Service.IService
{
	public interface IProcessService
	{
		List<Process> GetAllProcess();
		Process GetProcessById(int id);
		public List<Process> GetAllProcessByProduceId(int produceId);

        void AddProcess(ProcessRequestDTO process);
		void UpdateProcess(int id, ProcessRequestDTO process);
		void DeleteProcess(int id);
		IEnumerable<Process> SearchProcess(string name);
	}
}
