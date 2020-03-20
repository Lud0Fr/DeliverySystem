using AutoMapper;
using DeliverySystem.Api.Dtos;
using DeliverySystem.Domain.Deliveries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeliverySystem.Api.Queries
{
    #region Interface

    public interface IDeliveryQueries
    {
        Task<DeliveryDto> GetAsync(int deliveryId);
        Task<IEnumerable<DeliveryDto>> GetAllAsync();
    }

    #endregion

    public class DeliveryQueries : IDeliveryQueries
    {
        private readonly IDeliveryRepository _deliveryRepository;
        private readonly IMapper _mapper;

        public DeliveryQueries(
            IDeliveryRepository deliveryRepository,
            IMapper mapper)
        {
            _deliveryRepository = deliveryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DeliveryDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<DeliveryDto>>(await _deliveryRepository.GetAllAsync());
        }

        public async Task<DeliveryDto> GetAsync(int deliveryId)
        {
            return _mapper.Map<DeliveryDto>(await _deliveryRepository.GetAsync(deliveryId));
        }
    }
}
