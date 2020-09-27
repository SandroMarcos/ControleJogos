using FluentNHibernate.Mapping;

namespace AcessoADados
{
    public class EmprestimoJogoMap : ClassMap<EmprestimoJogo>
    {
        public EmprestimoJogoMap()
        {
            this.Table("EmprestimoJogo");
            this.LazyLoad();

            Id(x => x.IdEmprestimoJogo).GeneratedBy.Native().Column("IdEmprestimoJogo");
            References(x => x.Usuario).Column("IdUsuario");
            References(x => x.Jogo).Column("IdJogo");
            Map(x => x.DataEmprestimo).Column("DataEmprestimo").Not.Nullable();
            Map(x => x.DataDevolucao).Column("DataDevolucao").Nullable();            
        }
    }
}
