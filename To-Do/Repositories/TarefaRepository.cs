using System;
using System.Threading.Tasks;
using To_Do.Data.Context;
using To_Do.Interfaces;
using To_Do.Models.Entities;
using Mapster;
using To_Do.DTOs;
using System.Data.Entity;
using System.Collections.Generic;
using To_Do.Mapping;

namespace To_Do.Repositories
{
    public class TarefaRepository : IDisposable, ITarefa<TarefaDTO>
    {
        private readonly ApplicationContext dbContext;
        public TarefaRepository() 
        {
            dbContext = new ApplicationContext();
            ConfigurationMapSter.ConfigurarMapeamento();
        }

        public async Task<List<TarefaDTO>> GetAllAsync()
        {
            var result = await dbContext.Tarefas.AsNoTracking().ProjectToType<TarefaDTO>().ToListAsync();

            return result;
        }


        public async Task<TarefaDTO> GetAsync(int id)
        {
            var result = await dbContext.Tarefas.FindAsync(id);

            if (result == null) return null;

            var tarefaDTO = result.Data.Adapt<TarefaDTO>();

            return tarefaDTO;
        }


        public async Task<TarefaDTO> CreateAsync(TarefaDTO tarefa)
        {
            var task = tarefa.Adapt<Tarefa>();

            var result = dbContext.Tarefas.Add(task);
            await dbContext.SaveChangesAsync();

            var tarefaDTO = result.Adapt<TarefaDTO>();
            return tarefaDTO;
        }


        public async Task<TarefaDTO> PatchAsync(TarefaDTO tarefa)
        {
            var result = await dbContext.Tarefas.FindAsync(tarefa.Id);

            if (result == null) return null;

            if (tarefa.Titulo != null)
                result.Titulo = tarefa.Titulo;

            if (tarefa.Descricao != null)
                result.Descricao = tarefa.Descricao;

            if (tarefa.Categoria.HasValue)
                result.Categoria = tarefa.Categoria.Value;

            if (tarefa.Data.HasValue)
                result.Data = tarefa.Data.Value;

            if (tarefa.Status.HasValue)
                result.Status = tarefa.Status.Value;

            await dbContext.SaveChangesAsync();
            var tarefaDTO = result.Adapt<TarefaDTO>();

            return tarefaDTO;
        }


        public async Task<TarefaDTO> DeleteAsync(int id)
        {
            var result = await dbContext.Tarefas.FindAsync(id);

            if (result == null) return null;

            var tarefaDTO = result.Adapt<TarefaDTO>();

            dbContext.Tarefas.Remove(result);
            await dbContext.SaveChangesAsync();

            return tarefaDTO;
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }

    }
}