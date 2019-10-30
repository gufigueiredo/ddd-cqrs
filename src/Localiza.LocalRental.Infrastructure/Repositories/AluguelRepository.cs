using System;
using System.Collections.Generic;
using AutoMapper;
using LiteDB;
using Localiza.LocalRental.Domain.Model.Aluguel;
using Localiza.LocalRental.Infrastructure.DataAccess;
using Localiza.LocalRental.Infrastructure.DataModel.Collections;
using Localiza.LocalRental.Infrastructure.Events;

namespace Localiza.LocalRental.Infrastructure.Repositories
{
    public class AluguelRepository : IAluguelRepository
    {
        private readonly ILiteDbContext _db;
        private readonly IEventStream _eventStream;
        private readonly IMapper _mapper;

        public AluguelRepository(ILiteDbContext context, IEventStream eventStream, IMapper mapper)
        {
            _db = context;
            _eventStream = eventStream;
            _mapper = mapper;
        }

        public IEnumerable<Aluguel> ListarTodos()
        {
            using (var db = new LiteRepository(_db.Context))
            {
                var dbModel = db.Query<AluguelDbModel>()
                    .Include(x => x.Opcionais)
                    .ToList();

                return _mapper.Map<List<Aluguel>>(dbModel);
            }
        }

        public IEnumerable<Aluguel> ListarPorCliente(string clienteId)
        {
            using (var db = new LiteRepository(_db.Context))
            {
                var dbModel = db.Query<AluguelDbModel>()
                    .Include(x => x.Opcionais)
                    .Where(p => p.ClienteId == clienteId)
                    .ToList();

                return _mapper.Map<List<Aluguel>>(dbModel);
            }
        }

        public Aluguel ObterPorId(string id)
        {
            using (var db = new LiteRepository(_db.Context))
            {
                var dbModel = db.Query<AluguelDbModel>()
                    .Include(x => x.Opcionais)
                    .Where(p => p.Id == id)
                    .FirstOrDefault();

                return _mapper.Map<Aluguel>(dbModel);
            }
        }

        public Aluguel ObterPorNumeroControle(string numeroControle)
        {
            using (var db = new LiteRepository(_db.Context))
            {
                var dbModel = db.Query<AluguelDbModel>()
                    .Include(x => x.Opcionais)
                    .Where(p => p.NumeroControle == numeroControle)
                    .FirstOrDefault();

                return _mapper.Map<Aluguel>(dbModel);
            }
        }

        public void Add(Aluguel entity)
        {
            using (var db = new LiteRepository(_db.Context))
            {
                var dbModel = _mapper.Map<AluguelDbModel>(entity);
                db.Insert(dbModel);
            }
            _eventStream.AddToStream(entity);
        }

        public void Remove(string objectKey)
        {
            using (var db = new LiteRepository(_db.Context))
            {
                db.Delete<AluguelDbModel>(new BsonValue(objectKey));
            }
        }

        public void Update(Aluguel entity)
        {
            using (var db = new LiteRepository(_db.Context))
            {
                var dbModel = _mapper.Map<AluguelDbModel>(entity);
                db.Update(dbModel);
            }
            _eventStream.AddToStream(entity);
        }
    }
}
