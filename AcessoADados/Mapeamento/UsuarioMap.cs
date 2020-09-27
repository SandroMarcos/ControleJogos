using FluentNHibernate.Mapping;

namespace AcessoADados
{
    public class UsuarioMap : ClassMap<Usuario>
    {
        public UsuarioMap()
        {
            this.Table("Usuario");
            this.LazyLoad();

            Id(x => x.IdUsuario).GeneratedBy.Native().Column("IdUsuario");            
            Map(x => x.Nome).Column("Nome").Not.Nullable().Length(150);
            Map(x => x.Email).Column("Email").Not.Nullable().Length(100);
            Map(x => x.Senha).Column("Senha").Not.Nullable().Length(50);

            HasMany(x => x.Jogos).KeyColumn("IdUsuario").ForeignKeyConstraintName("none").Inverse();
            HasMany(x => x.Emprestimos).KeyColumn("IdUsuario").ForeignKeyConstraintName("none").Inverse();
        }
    }
}
