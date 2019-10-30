using System;
using System.Collections.Generic;
using AutoMapper;
using LiteDB;
using Localiza.LocalRental.Domain.Model.Cliente;
using Localiza.LocalRental.Infrastructure.DataAccess;
using Localiza.LocalRental.Infrastructure.DataModel.Collections;

namespace Localiza.LocalRental.Infrastructure.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        readonly ILiteDbContext _db;
        readonly IMapper _mapper;

        public ClienteRepository(ILiteDbContext context, IMapper mapper)
        {
            _db = context;
            _mapper = mapper;
        }

        public IEnumerable<Cliente> ListarTodos()
        {
            using (var db = new LiteRepository(_db.Context))
            {
                var dbModel = db.Query<ClienteDbModel>()
                    .ToList();

                return _mapper.Map<List<Cliente>>(dbModel);
            }
        }

        public Cliente ObterPorId(string id)
        {
            using (var db = new LiteRepository(_db.Context))
            {
                var dbModel = db.Query<ClienteDbModel>()
                    .Where(p => p.Id == id)
                    .FirstOrDefault();

                return _mapper.Map<Cliente>(dbModel);
            }
        }

        public void Add(Cliente entity)
        {
            using (var db = new LiteRepository(_db.Context))
            {
                var dbModel = _mapper.Map<ClienteDbModel>(entity);
                db.Insert(dbModel);
            }
        }

        public void Remove(string objectKey)
        {
            using (var db = new LiteRepository(_db.Context))
            {
                db.Delete<ClienteDbModel>(new BsonValue(objectKey));
            }
        }

        public void Update(Cliente entity)
        {
            using (var db = new LiteRepository(_db.Context))
            {
                var dbModel = _mapper.Map<ClienteDbModel>(entity);
                db.Update(dbModel);
            }
        }
    }
}
