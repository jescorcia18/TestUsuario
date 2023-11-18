using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestUsuarios.Models.Pagination;
using TestUsuarios.Models.Usuarios;
using TesUsuarios.Data.DBContext;
using TesUsuarios.Data.Entities;

namespace TesUsuarios.Data.Repository.Usuarios
{
    public class UsuarioRepository:IUsuarioRepository
    {
        #region Global Variables
        private readonly DatabaseContext _context;
        #endregion
        #region Constructor Method
        public UsuarioRepository(DatabaseContext context)
        {
            _context = context;
        }
        #endregion
        #region Public Methods
        public async Task<UsuariosDataModel> Create(UsuariosDataModel entity)
        {
            try
            {
                var result = await _context.AddAsync(entity);
                await _context.SaveChangesAsync();
                return result.Entity;

            }
            catch (Exception ex)
            {
                throw new Exception($"Error in Repository: {ex.Message}");
            }
        }

        public async Task<UsuariosDataModel?> Read(int id)
        {
            try
            {
                return await _context.Usuarios.Where(u => u.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in Repository: {ex.Message}");
            }
        }

        public async Task<UsuariosDataModel> Update(int id, UsuariosDataModel entity)
        {
            try
            {
                if (await _context.Usuarios.FindAsync(id) is UsuariosDataModel found)
                {
                    _context.Entry(found).CurrentValues.SetValues(entity);
                    await _context.SaveChangesAsync();
                }
                return entity;

            }
            catch (Exception ex)
            {
                throw new Exception($"Error in Repository: {ex.Message}");
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                var entity = await Read(id);
                if (entity != null)
                {
                    await Task.FromResult(_context.Usuarios.Remove(entity));
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                throw new Exception($"Error in Repository: {ex.Message}");
            }
        }

        public async Task<List<UsuariosDataModel>> Search(UsuarioRequest searchRequest, Paginator paginator, Sorter sorter, bool validateEmail = false)
        {
            try
            {
                var query = await GetQuery(searchRequest, validateEmail);
                query = await SortQuery(query, sorter);

                return await query.Skip((paginator.PageNumber - 1) * paginator.PageSize)
                   .Take(paginator.PageSize).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in Repository: {ex.Message}");
            }
        }
        public async Task<int> TotalCount(UsuarioRequest searchRequest)
        {
            return await GetQuery(searchRequest).Result.CountAsync();
        }
        #endregion

        #region Private Methods
        private async Task<IQueryable<UsuariosDataModel>> GetQuery(UsuarioRequest searchRequest, bool validateEmail = false)
        {
            var query = await Task.Run(() => from usuarios in _context.Usuarios select usuarios);

            if (validateEmail)
            {
                query = query.Where(u => u.Email.ToLower().Trim().Equals(searchRequest.Email.ToLower().Trim()));
                return query;
            }

            if (!string.IsNullOrEmpty(searchRequest.Name))
                query = query.Where(u => u.Name.ToLower().Trim().Contains(searchRequest.Name.ToLower().Trim()));

            if (searchRequest.Age>0)
                query = query.Where(u => u.Age.Equals(searchRequest.Age));

            if (!string.IsNullOrEmpty(searchRequest.Email))
                query = query.Where(c => c.Email.ToLower().Trim().Contains(searchRequest.Email.ToLower().Trim()));

            return await Task.FromResult(query);
        }

        private async Task<IQueryable<UsuariosDataModel>> SortQuery(IQueryable<UsuariosDataModel> query, Sorter sorter)
        {
            switch (sorter.SortBy)
            {
                case "name":
                    if (sorter.SortOrder.ToLower().Trim().Equals("asc"))
                        query = query.OrderBy(c => c.Name);
                    else
                        query = query.OrderByDescending(c => c.Name);
                    break;

                case "age":
                    if (sorter.SortOrder.ToLower().Trim().Equals("asc"))
                        query = query.OrderBy(c => c.Age);
                    else
                        query = query.OrderByDescending(c => c.Age);
                    break;

                case "email":
                    if (sorter.SortOrder.ToLower().Trim().Equals("asc"))
                        query = query.OrderBy(c => c.Email);
                    else
                        query = query.OrderByDescending(c => c.Email);
                    break;
            }

            return await Task.FromResult(query);
        }
        #endregion

    }
}
