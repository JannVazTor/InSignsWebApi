using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InSigns_WebApi.Models
{
    public class ModelFactory
    {

        public List<PhraseModel> Create(List<phrase> phrases)
        {
            return phrases.Select(item => new PhraseModel()
            {
                Id = item.id,
                Description = item.description,
                VideoUrl = item.videoUrl
            }).ToList();
        }

        public List<SituationsModel> Create(List<situation> situations)
        {
            return situations.Select(item => new SituationsModel()
            {
                Id = item.id,
                Name = item.name,
                Description = item.description,
                Phrases = Create(new List<phrase>(item.phrases.Where(p => p.situation_id == item.id)))               
            }).ToList();
        }


        public class SituationsModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public List<PhraseModel> Phrases { get; set; }
        }
        public class PhraseModel
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public string VideoUrl { get; set; }
        }
    }
}