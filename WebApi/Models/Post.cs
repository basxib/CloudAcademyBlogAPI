using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace WebApi.Models{
    public class Post{
        [Required]
        public Guid Id{get;set;}

        [Required]
        public string Title{get;set;}
        [Required]
        public string Content{get;set;}
        [Required]
        public string Author{get; set;}
        public string Image{get;set;}
        [Required]
        public string Category{get;set;}
        public List<string> Tags{get;set;}
        [Required]
        public DateTime CreatedAt{get;set;}
        [Required]
        public DateTime ModifiedAt{get;set;}

        public bool IsValid()
        {
            if(string.IsNullOrEmpty(Content) || Content.Length > 1024 || string.IsNullOrEmpty(Title) || string.IsNullOrEmpty(Author) || string.IsNullOrEmpty(Category))
                return false;
            else return true;
        }
        public Post()
        {}
    }
    
}