using System;
using System.Collections.Generic;
using AutoMapper;
using LiteDB;
using Localiza.LocalRental.Domain.Model.Fatura;
using Localiza.LocalRental.Infrastructure.DataAccess;
using Localiza.LocalRental.Infrastructure.DataModel.Collections;

namespace Localiza.LocalRental.Infrastructure.Repositories
{
    public class FaturaRepository : IFaturaRepository
    {
        readonly ILiteDbContext _db;
        readonly IMapper _mapper;

        public FaturaRepository(ILiteDbContext context, IMapper mapper)
        {
            _db = context;
            _mapper = mapper;
        }

        public IEnumerable<Fatura> ListarPorCliente(string clienteId)
        {
            using (var db = new LiteRepository(_db.Context))
            {
                var dbModel = db.Query<FaturaDbModel>()
                    .Include(x => x.Cobrancas)
                    .Include(x => x.Pagamento)
                    .Where(x => x.ClienteId == clienteId)
                    .ToList();

                return _mapper.Map<List<Fatura>>(dbModel);
            }
        }

        public IEnumerable<Fatura> ListarTodos()
        {
            using (var db = new LiteRepository(_db.Context))
            {
                var dbModel = db.Query<FaturaDbModel>()
                    .Include(x => x.Cobrancas)
                    .Include(x => x.Pagamento)
                    .ToList();

                return _mapper.Map<List<Fatura>>(dbModel);
            }
        }

        public Fatura ObterPorId(string id)
        {
            using (var db = new LiteRepository(_db.Context))
            {
                var dbModel = db.Query<FaturaDbModel>()
                    .Include(x => x.Cobrancas)
                    .Include(x => x.Pagamento)
                    .Where(p => p.Id == id)
                    .FirstOrDefault();

                return _mapper.Map<Fatura>(dbModel);
            }
        }

        public void Add(Fatura entity)
        {
            using (var db = new LiteRepository(_db.Context))
            {
                var dbModel = _mapper.Map<FaturaDbModel>(entity);
                db.Insert(dbModel);
            }
        }

        public void Remove(string objectKey)
        {
            using (var db = new LiteRepository(_db.Context))
            {
                db.Delete<FaturaDbModel>(new BsonValue(objectKey));
            }
        }

        public void Update(Fatura entity)
        {
            using (var db = new LiteRepository(_db.Context))
            {
                var dbModel = _mapper.Map<FaturaDbModel>(entity);
                db.Update(dbModel);
            }
        }
    }
}
