using System.Linq;
using AutoMapper;
using Localiza.LocalRental.Domain.Model.Aluguel;
using Localiza.LocalRental.Domain.Model.Cliente;
using Localiza.LocalRental.Domain.Model.Fatura;
using Localiza.LocalRental.Domain.Model.Veiculo;
using Localiza.LocalRental.Infrastructure.DataModel.Collections;
using SC.SDK.NetStandard.DomainCore;

namespace Localiza.LocalRental.Infrastructure.DataModel
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Aluguel
            CreateMap<AluguelDbModel, Aluguel>()
                .ForMember(dest => dest.Opcionais,
                    opt => opt.MapFrom(src => src.Opcionais.Select(
                        value =>  Enumeration.FromValue<Opcional>(value))
                    )
                )
                .ForMember(dest => dest.Situacao,
                    opt => opt.MapFrom(src => Enumeration.FromValue<SituacaoAluguel>(src.Situacao)
                    )
                );

            CreateMap<Aluguel, AluguelDbModel>()
                .ForMember(dest => dest.Opcionais,
                    opt => opt.MapFrom(src => src.Opcionais.Select(e => e.Id))
                )
                .ForMember(dest => dest.Situacao,
                    opt => opt.MapFrom(src => src.Situacao.Id)
                );

            //Veiculo
            CreateMap<VeiculoDbModel, Veiculo>();
            CreateMap<Veiculo, VeiculoDbModel>();
            CreateMap<ModeloVeiculo, ModeloVeiculoDbModel>()
                .ForMember(dest => dest.Montadora,
                    opt => opt.MapFrom(src => src.Montadora.Id)
                );
            CreateMap<ModeloVeiculoDbModel, ModeloVeiculo>()
                .ForCtorParam("montadora", opt => opt.MapFrom(src => Enumeration.FromValue<Montadora>(src.Montadora)))
                .ForMember(dest => dest.Montadora,
                    opt => opt.MapFrom(src => Enumeration.FromValue<Montadora>(src.Montadora)
                    )
                );
            CreateMap<GrupoVeiculo, GrupoVeiculoDbModel>();
            CreateMap<GrupoVeiculoDbModel, GrupoVeiculo>();

            //Fatura
            CreateMap<FaturaDbModel, Fatura>();
            CreateMap<Fatura, FaturaDbModel>();
            CreateMap<PagamentoFaturaDbModel, PagamentoFatura>();
            CreateMap<PagamentoFatura, PagamentoFaturaDbModel>();
            CreateMap<Cobranca, CobrancaDbModel>();
            CreateMap<CobrancaDbModel, Cobranca>().DisableCtorValidation();

            //Cliente
            CreateMap<Cliente, ClienteDbModel>()
                .ForMember(dest => dest.TelefoneDdd,
                    opt => opt.MapFrom(src => src.Telefone.Ddd)
                )
                .ForMember(dest => dest.TelefoneNumero,
                    opt => opt.MapFrom(src => src.Telefone.Numero)
                )
                .ForMember(dest => dest.Cpf,
                    opt => opt.MapFrom(src => src.Cpf.Numero)
                );

            CreateMap<ClienteDbModel, Cliente>()
                .ForCtorParam("cpf",
                    opt => opt.MapFrom(src => new CPF(src.Cpf)))
                .ForMember(dest => dest.Telefone,
                    opt => opt.MapFrom(src => new Telefone(src.TelefoneDdd, src.TelefoneNumero))
                );
                
        }
    }
}
