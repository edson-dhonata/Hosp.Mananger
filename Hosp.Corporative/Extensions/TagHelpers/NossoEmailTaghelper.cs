using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Hosp.Corporative.Extensions.TagHelpers
{
    // Para criar uma TagHelper:
    // 1) Criar uma classe e fazer ela herdar de TagHelper;
    // 2) Fazer um override no método ProceesAsync;

    public class NossoEmailTaghelper : TagHelper
    {
        public string Dominio { get; set; } = "eaditdeveloper.com.br"; // Cria atributo de valor na minha taghelper (PARÂMETROS)

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a"; //Nome da Tag

            var prefixo = await output.GetChildContentAsync(); //Pega conteúdo entre as TAGS

            var meuEmail = prefixo.GetContent() + "@" + Dominio;

            output.Attributes.SetAttribute("href", "mailto:" + meuEmail); // Cria atributos nas tags

            output.Content.SetContent(meuEmail); //Saída do conteúdo com o email.
        }
    }
}
