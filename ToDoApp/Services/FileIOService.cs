using Newtonsoft.Json;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using ToDoApp.Models;

namespace ToDoApp.Services
{
    class FileIOService
    {
        private readonly string _filePath;

        public FileIOService(string path)
        {
            _filePath = path;
        }

        public async Task<BindingList<ToDoModel>> LoadDataAsync()
        {
            var fileExists = File.Exists(_filePath);
            if (!fileExists)
            {
                using (var file = File.CreateText(_filePath)) { }
                return new BindingList<ToDoModel>();
            }
            using (var reader = File.OpenText(_filePath))
            {
                var fileText = await reader.ReadToEndAsync();
                return JsonConvert.DeserializeObject<BindingList<ToDoModel>>(fileText);
            }
        }

        public async Task SaveDataAsync(object todoDataList)
        {
            using (var writer = File.CreateText(_filePath))
            {
                var output = JsonConvert.SerializeObject(todoDataList);
                await writer.WriteAsync(output);
            }
        }
    }
}