
namespace Yuuna.ControlFlow
{
    public static class CallbackStatusFactory
    {
        public static ICallbackStatus Failure(Message message, MoodKinds mood)
        {
            return new InternalFailure(message,mood);
        }

        public static ICallbackStatus Success(Message message, MoodKinds mood)
        {
            return new InternalSuccess(message, mood);
        }

        public static ICallbackStatus Context(Message message, MoodKinds mood)
        {
            return new InternalContext(message, mood);
        }


        private sealed class InternalFailure : ICallbackStatus
        {
            internal InternalFailure(Message message, MoodKinds mood)
            {
                this.Message = message;
                this.Mood = mood;
            }
            public Message Message { get; }

            public MoodKinds Mood { get; }
        }

        private sealed class InternalContext : ICallbackStatus
        {
           internal InternalContext (Message message, MoodKinds mood)
            {
                this.Message = message;
                this.Mood = mood;
            }
        public Message Message { get; }

        public MoodKinds Mood { get; }
    }

        private sealed class InternalSuccess : ICallbackStatus
        {
            internal InternalSuccess(Message message, MoodKinds mood)
            {
                this.Message = message;
                this.Mood = mood;
            }
            public Message Message { get; }

            public MoodKinds Mood { get; }
        }
            
    }


}