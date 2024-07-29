using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagementSystem.Validations
{
    public class Requisicao
    {
        public static bool IdRequisicaoIgualIdCorpoRequisicao(int IdRequisicao, int IdCorpoRequisicao)
        {
            return IdRequisicao == IdCorpoRequisicao;
        }
    }
}