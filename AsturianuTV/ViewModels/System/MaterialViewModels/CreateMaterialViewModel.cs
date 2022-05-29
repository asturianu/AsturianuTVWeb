using AsturianuTV.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Http;

namespace AsturianuTV.ViewModels.System.MaterialViewModels
{
    public class CreateMaterialViewModel
    {
        public bool IsNewsMaterial { get; set; }
        public IFormFile FilePath { get; set; }
    }
}
