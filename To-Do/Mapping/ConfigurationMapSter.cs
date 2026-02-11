using To_Do.DTOs;
using To_Do.Models.Entities;
using Mapster;

namespace To_Do.Mapping
{
    public static class ConfigurationMapSter
    {
        public static void ConfigurarMapeamento()
        {
            TypeAdapterConfig<Tarefa, TarefaDTO>.NewConfig();
            TypeAdapterConfig<TarefaDTO, Tarefa>.NewConfig();
        }
    }
}