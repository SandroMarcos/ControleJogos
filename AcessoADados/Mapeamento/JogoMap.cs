using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcessoADados
{
    public class JogoMap : ClassMap<Jogo>
    {
        public JogoMap()
        {
            this.Table("Jogo");
            this.LazyLoad();

            Id(x => x.IdJogo).GeneratedBy.Native().Column("IdJogo");
            References(x => x.Usuario).Column("IdUsuario");
            Map(x => x.Nome).Column("Nome").Not.Nullable().Length(250);
            Map(x => x.Descricao).Column("Descricao").Not.Nullable().Length(250);
            Map(x => x.Emprestado).Column("Emprestado").Not.Nullable().Default("0");            

            HasMany(x => x.Emprestimos).KeyColumn("IdJogo").ForeignKeyConstraintName("none").Inverse();
        }
    }
}
