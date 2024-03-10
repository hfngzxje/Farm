using Farm.DTOS;
using Farm.Modelss;
using Farm.Service.IService;
using Microsoft.EntityFrameworkCore;

namespace Farm.Service
{
	public class ProcessService : IProcessService
	{
		private readonly FarmContext _context;

		public ProcessService(FarmContext context)
		{
			_context = context;
		}

		public void AddProcess(ProcessRequestDTO process)
		{
			var newProcess = new Process
			{
				Name = process.Name,
				Description = process.Description,
				StartTime = process.StartTime,
				EndTime = process.EndTime,
				ProduceId = process.ProduceId,
				Status = process.Status
			};

			_context.Processes.Add(newProcess);
			_context.SaveChanges();
		}

		public void DeleteProcess(int id)
		{
			var processToDelete = _context.Processes.FirstOrDefault(p => p.ProcessId == id);
			if (processToDelete != null)
			{
				_context.Processes.Remove(processToDelete);
				_context.SaveChanges();
			}
		}

		public List<Process> GetAllProcess()
		{
			return _context.Processes.ToList();
		}

		public Process GetProcessById(int id)
		{
			return _context.Processes.FirstOrDefault(p => p.ProcessId == id);
		}

        public List<Process> GetAllProcessByProduceId(int produceId)
        {
            try
            {
                var processes = _context.Processes.Where(p => p.ProduceId == produceId).ToList();
                return processes;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

		public List<Process> SearchProcess(string? name, int? produceId)
		{
			if (!string.IsNullOrEmpty(name) && produceId.HasValue)
			{
				return _context.Processes.Where(p => p.Name.Contains(name) && p.ProduceId == produceId.Value).ToList();
			}
			else if (!string.IsNullOrEmpty(name))
			{
				return _context.Processes.Where(p => p.Name.Contains(name)).ToList();
			}
			else if (produceId.HasValue)
			{
				return _context.Processes.Where(p => p.ProduceId == produceId.Value).ToList();
			}
			else
			{
				return _context.Processes.ToList();
			}
		}

		public void UpdateProcess(int id, ProcessRequestDTO process)
		{
			var existingProcess = _context.Processes.FirstOrDefault(p => p.ProcessId == id);
			if (existingProcess != null)
			{
				existingProcess.Name = process.Name;
				existingProcess.Description = process.Description;
				existingProcess.StartTime = process.StartTime;
				existingProcess.EndTime = process.EndTime;
				existingProcess.ProduceId = process.ProduceId;
				existingProcess.Status = process.Status;

				_context.SaveChanges();
			}
		}
	}
}
