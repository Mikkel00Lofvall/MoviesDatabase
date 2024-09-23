using System.Runtime.CompilerServices;
using Microsoft.Identity.Client;
using MoviesDatabase.Interfaces;

namespace MoviesDatabase.Models.Test
{
    public class TestModel : IEntity
    {
        public int id {get; set;}
        public string Message { get; set; }
        public bool Result { get; set; }
    }
}