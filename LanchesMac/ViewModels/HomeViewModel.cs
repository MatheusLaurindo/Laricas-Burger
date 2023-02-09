using LanchesMac.Models;

namespace LanchesMac.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Lanche> LanchesPreferidos { get; set; }
        public int LancheId { get; set; }
    }
}
