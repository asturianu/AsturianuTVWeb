using AsturianuTV.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Http;

namespace AsturianuTV.ViewModels.System.MaterialViewModels
{
    public class CreateMaterialViewModel
    {
        public int? BlogId { get; set; }
        public Blog Blog { get; set; }
        public int? NewsId { get; set; }
        public News News { get; set; }
        public bool IsNewsMaterial { get; set; }
        public IFormFileCollection FilePaths { get; set; }
    }
}
