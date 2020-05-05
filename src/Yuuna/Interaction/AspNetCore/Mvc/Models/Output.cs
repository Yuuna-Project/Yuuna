// Author: Yuuna-Project@Orlys
// Github: github.com/Orlys
// Contact: orlys@yuuna-project.com

namespace Yuuna.Interaction.AspNetCore.Mvc.Models
{
    using Yuuna.Contracts.Interaction;

    public class Output
    {
        public string Message { get; }

        public string Mood { get; }

        public bool Success { get; }

        public string Text { get; }

        public Output(bool success, string message, Mood mood, string text)
        {
            this.Success = success;
            this.Message = message;
            this.Mood = mood.Name;
            this.Text = text;
        }
    }
}