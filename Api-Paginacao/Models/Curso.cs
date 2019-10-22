using Api_Paginacao.Models.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api_Paginacao.Models
{
    [Table("Curso")]
    public class Curso
    {   
        public int Id { get; set; }

        [Required(ErrorMessage = "O título do curso deve ser preenchido.")]
        [MaxLength(100, ErrorMessage = "O título do curso deve ter no máximo 100 caracteres.")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "A URL do curso deve ser preenchida")]
        [Url(ErrorMessage ="A URL do curso deve ser um endereço valido")]
        public string URL { get; set; }

        [Required(ErrorMessage = "O canal do curso deve ser preenchido")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ECanal Canal { get; set; }

        [Required(ErrorMessage = "A data de publicação do curso deve ser preenchida")]
        public DateTime DataPublicacao { get; set; }

        [Required(ErrorMessage ="A Carga horária do curso deve ser preenchida")]
        [Range(1, Int32.MaxValue, ErrorMessage ="Minimo da 1 hora de carga horária")]
        public int CargaHoraria { get; set; }

    }
}