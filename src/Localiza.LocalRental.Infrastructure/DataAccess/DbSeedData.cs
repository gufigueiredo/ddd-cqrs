using System;
using System.Collections.Generic;
using LiteDB;
using Localiza.LocalRental.Infrastructure.DataModel.Collections;

namespace Localiza.LocalRental.Infrastructure.DataAccess
{
    public static class DbSeedData
    {
        public static void Seed(ILiteDbContext database)
        {
            using (var db = new LiteRepository(database.Context))
            {
                var collection = new List<VeiculoDbModel>
                {
                    new VeiculoDbModel
                    {
                        Id = Guid.NewGuid().ToString(),
                        Cor = 1,
                        ModeloVeiculo = new ModeloVeiculoDbModel()
                        {
                            Grupo = new GrupoVeiculoDbModel
                            {
                                Nome = "Economico",
                                MultiplicadorHoraBase = 1
                            },
                            Modelo = "Uno",
                            Montadora = 1
                        },
                        NumeroPlaca = "PLU2730"

                    },
                    new VeiculoDbModel
                    {
                        Id = Guid.NewGuid().ToString(),
                        Cor = 2,
                        ModeloVeiculo = new ModeloVeiculoDbModel()
                        {
                            Grupo = new GrupoVeiculoDbModel
                            {
                                Nome = "Plus",
                                MultiplicadorHoraBase = 2.2M
                            },
                            Modelo = "HB20",
                            Montadora = 5
                        },
                        NumeroPlaca = "PYZ9845"
                    },
                    new VeiculoDbModel
                    {
                        Id = Guid.NewGuid().ToString(),
                        Cor = 2,
                        ModeloVeiculo = new ModeloVeiculoDbModel()
                        {
                            Grupo = new GrupoVeiculoDbModel
                            {
                                Nome = "Plus",
                                MultiplicadorHoraBase = 2.2M
                            },
                            Modelo = "Onix",
                            Montadora = 3
                        },
                        NumeroPlaca = "OMH4394"
                    },
                    new VeiculoDbModel
                    {
                        Id = Guid.NewGuid().ToString(),
                        Cor = 2,
                        ModeloVeiculo = new ModeloVeiculoDbModel()
                        {
                            Grupo = new GrupoVeiculoDbModel
                            {
                                Nome = "Plus",
                                MultiplicadorHoraBase = 2.2M
                            },
                            Modelo = "Ka",
                            Montadora = 4
                        },
                        NumeroPlaca = "PYZ9845"
                    },
                    new VeiculoDbModel
                    {
                        Id = Guid.NewGuid().ToString(),
                        Cor = 3,
                        ModeloVeiculo = new ModeloVeiculoDbModel()
                        {
                            Grupo = new GrupoVeiculoDbModel
                            {
                                Nome = "Sedan",
                                MultiplicadorHoraBase = 2.7M
                            },
                            Modelo = "Virtus",
                            Montadora = 2
                        },
                        NumeroPlaca = "OMB6490"
                    },
                    new VeiculoDbModel
                    {
                        Id = Guid.NewGuid().ToString(),
                        Cor = 2,
                        ModeloVeiculo = new ModeloVeiculoDbModel()
                        {
                            Grupo = new GrupoVeiculoDbModel
                            {
                                Nome = "Sedan",
                                MultiplicadorHoraBase = 2.7M
                            },
                            Modelo = "Cronos",
                            Montadora = 1
                        },
                        NumeroPlaca = "PKE1319"
                    },
                    new VeiculoDbModel
                    {
                        Id = Guid.NewGuid().ToString(),
                        Cor = 2,
                        ModeloVeiculo = new ModeloVeiculoDbModel()
                        {
                            Grupo = new GrupoVeiculoDbModel
                            {
                                Nome = "SUV",
                                MultiplicadorHoraBase = 3.5M
                            },
                            Modelo = "Ecosport",
                            Montadora = 4
                        },
                        NumeroPlaca = "PGX6471"
                    },
                    new VeiculoDbModel
                    {
                        Id = Guid.NewGuid().ToString(),
                        Cor = 2,
                        ModeloVeiculo = new ModeloVeiculoDbModel()
                        {
                            Grupo = new GrupoVeiculoDbModel
                            {
                                Nome = "Pickup",
                                MultiplicadorHoraBase = 5M
                            },
                            Modelo = "Amarok",
                            Montadora = 2
                        },
                        NumeroPlaca = "PLO7756"
                    }
                };
                db.Insert<VeiculoDbModel>(collection);
            }
        }
    }
}
