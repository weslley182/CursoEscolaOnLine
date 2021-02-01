using System;
using Xunit;

namespace CursoOnLine.DominioTest._Util
{
    public static class AssertExtension
    {
        public static void ComMensagem(this ArgumentException exception, string mensagem)
        {
            if(exception.Message == mensagem)
            {
                Assert.True(true);
                return;
            }
            Assert.False(true, $"Mensagem esperada: '{exception.Message}', mas recebeu a mensagem'{mensagem}'");
        }
    }
}
