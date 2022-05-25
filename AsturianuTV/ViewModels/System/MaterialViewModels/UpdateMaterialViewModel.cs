using AsturianuTV.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Http;

namespace AsturianuTV.ViewModels.System.MaterialViewModels
{
    public class UpdateMaterialViewModel
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
        public IFormFileCollection FilePathes { get; set; }
    }
}
