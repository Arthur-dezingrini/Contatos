using Contatos.Enums;

namespace Contatos.Models
{
    public class IndexModel
    {
        private static int _nextId = 1;
        public int Id { get; private set; }
        public string Name { get; set; }
        public string Telefone { get; set; }
        public string Municipio { get; set; }
        public string Estado { get; set; }
        public string Email { get; set; }
        public Categoria Categoria { get; set; }

        public IndexModel()
        {
            Id = _nextId++;
        }
    }

}
