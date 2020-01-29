
namespace Yuuna.Interaction.AspNetCore.Mvc.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Yuuna.Contracts.Interaction;

    public class Output
    {
        public Output(bool success, string message, Mood mood, string text)
        {
            this.Success = success;
            this.Message = message;
            this.Mood = mood.Name;
            this.Text = text;
        }
        public bool Success { get; }
        public string Message { get; }
        public string Mood { get; }
        public string Text { get; }
    }
}
