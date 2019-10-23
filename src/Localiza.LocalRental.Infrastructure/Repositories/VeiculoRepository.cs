using System;
using System.Collections.Generic;
using AutoMapper;
using LiteDB;
using Localiza.LocalRental.Domain.Model.Veiculo;
using Localiza.LocalRental.Infrastructure.DataAccess;
using Localiza.LocalRental.Infrastructure.DataModel.Collections;

namespace Localiza.LocalRental.Infrastructure.Repositories
{
    public class VeiculoRepository : IVeiculoRepository
    {
        readonly ILiteDbContext _db;
        readonly IMapper _mapper;

        public VeiculoRepository(ILiteDbContext context, IMapper mapper)
        {
            _db = context;
            _mapper = mapper;
        }

        public IEnumerable<Veiculo> ListarTodos()
        {
            using (var db = new LiteRepository(_db.Context))
            {
                var dbModel = db.Query<VeiculoDbModel>()
                    .Include(x => x.ModeloVeiculo)
                    .ToList();

                return _mapper.Map<List<Veiculo>>(dbModel);
            }
        }

        public Veiculo ObterPorId(string id)
        {
            using (var db = new LiteRepository(_db.Context))
            {
                var dbModel = db.Query<VeiculoDbModel>()
                    .Include(x => x.ModeloVeiculo)
                    .Where(p => p.Id == id)
                    .FirstOrDefault();

                return _mapper.Map<Veiculo>(dbModel);
            }
        }

        public Veiculo ObterPorPlaca(string numeroPlaca)
        {
            using (var db = new LiteRepository(_db.Context))
            {
                var dbModel = db.Query<VeiculoDbModel>()
                    .Include(x => x.ModeloVeiculo)
                    .Where(p => p.NumeroPlaca == numeroPlaca)
                    .FirstOrDefault();

                return _mapper.Map<Veiculo>(dbModel);
            }
        }

        public void Add(Veiculo entity)
        {
            using (var db = new LiteRepository(_db.Context))
            {
                var dbModel = _mapper.Map<VeiculoDbModel>(entity);
                db.Insert(dbModel);
            }
        }

        public void Remove(string objectKey)
        {
            using (var db = new LiteRepository(_db.Context))
            {
                db.Delete<VeiculoDbModel>(new BsonValue(objectKey));
            }
        }

        public void Update(Veiculo entity)
        {
            using (var db = new LiteRepository(_db.Context))
            {
                var dbModel = _mapper.Map<VeiculoDbModel>(entity);
                db.Update(dbModel);
            }
        }
    }
}
