using AutoMapper;
using DemoProject.Models;
using DemoProject.Models.DTO;
using System.Collections.Generic;

namespace DemoProject
{
    public class MappingConfig : Profile
    {
        public MappingConfig() 
        {
            CreateMap<Book,BookGetDTO>();
            //CreateMap<List<Book>, List<BookGetDTO>>();
            //CreateMap<List<BookGetDTO>, List<Book>> ();
            CreateMap<Book, BookCreateDTO>();
            CreateMap<Book, BookUpdateDTO>();
            CreateMap<BookGetDTO,Book>();
            CreateMap<BookCreateDTO, Book>();
            CreateMap<BookUpdateDTO, Book>();
        }
    }
}
