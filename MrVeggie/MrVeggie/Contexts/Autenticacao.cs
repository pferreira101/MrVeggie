using MrVeggie.Models;
using MrVeggie.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrVeggie.Contexts {

    public class Autenticacao {

        private readonly UtilizadorContext _context_u;


        /// <summary>
        /// Construtor da classe Autenticacao
        /// </summary>
        /// <param name="context_u">Contexto do utilizador</param>
        public Autenticacao(UtilizadorContext context_u) {
            _context_u = context_u;
        }



        /// <summary>
        /// Método que valida o acesso de um utilizador, verificando as suas credenciais.
        /// </summary>
        /// <param name="u">Utilizador a verificar</param>
        /// <returns></returns>
        public bool validaUtilizador(Utilizador u) {
            u.password = MyHelpers.HashPassword(u.password);
            var returnedUser = _context_u.Utilizador.Where(b => b.email == u.email && b.password == u.password).FirstOrDefault();

            if (returnedUser == null) return false;

            return true;
        }



        /// <summary>
        /// Método que verifica se um email já está registado ou não.
        /// </summary>
        /// <param name="email">Email a verificar</param>
        /// <returns>true - se o email não estiver em uso</returns>
        private bool checkIfExists(string email) {

            if (_context_u.Utilizador
                           .Where(s => s.email == email)
                           .ToList().Count != 0) return false;

            else return true;
        }



        /// <summary>
        /// Método que trata do registo do utilizador.
        /// </summary>
        /// <param name="u">Utilizador a registar</param>
        /// <returns>true - se registado com sucesso</returns>
        public bool RegistaUtilizador(Utilizador u) {

            if (!checkIfExists(u.email)) return false;

            u.password = MyHelpers.HashPassword(u.password);
            _context_u.Add(u);
            _context_u.SaveChanges();

            return true;
        }



        /// <summary>
        /// Método que retorna a informação sobre um utilizador dado o seu email.
        /// </summary>
        /// <param name="email">Email do utilizador</param>
        /// <returns>Utilizador encontrado</returns>
        public Utilizador getUtilizador(string email) {

            return _context_u.Utilizador.Where(u => u.email.Equals(email)).First();
        }
    }
}
