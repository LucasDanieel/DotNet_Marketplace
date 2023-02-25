
using AutoMapper;
using DotNet.Marketplace.Application.DTOs;
using DotNet.Marketplace.Application.DTOs.Validations;
using DotNet.Marketplace.Application.Services.Interfaces;
using DotNet.Marketplace.Domain.Entity;
using DotNet.Marketplace.Domain.Integrations;
using DotNet.Marketplace.Domain.Repository;

namespace DotNet.Marketplace.Application.Services
{
    public class PersonImageService : IPersonImageService
    {
        private readonly IPersonImageRepository _personImageRepository;
        private readonly IPersonRepository _personRepository;
        private readonly ISavePersonImage _savePersonImage;
        private readonly IMapper _mapper;

        public PersonImageService(
            IPersonImageRepository personImageRepository, 
            IPersonRepository personRepository, 
            IMapper mapper, 
            ISavePersonImage savePersonImage)
        {
            _personImageRepository = personImageRepository;
            _personRepository = personRepository;
            _mapper = mapper;
            _savePersonImage = savePersonImage;
        }

        public async Task<ResultService<PersonImageDTO>> GetByIdAsync(int id)
        {
            var image = await _personImageRepository.GetByIdAsync(id);
            if (image == null)
                return ResultService.Fail<PersonImageDTO>("Imagem não encontrada");

            var personImage = _mapper.Map<PersonImageDTO>(image);
            return ResultService.Ok(personImage);
        }

        public async Task<ResultService<ICollection<PersonImageDTO>>> GetByPersonIdAsync(int personId)
        {
            var person = await _personRepository.GetByIdAsync(personId);
            if (person == null)
                return ResultService.Fail<ICollection<PersonImageDTO>>("Pessoa não encontrada");
         
            var image = await _personImageRepository.GetByPersonIdAsync(personId);
            if (image == null)
                return ResultService.Fail<ICollection<PersonImageDTO>>("Imagem não encontrada");

            var personImage = _mapper.Map<ICollection<PersonImageDTO>>(image);
            return ResultService.Ok(personImage);
        }

        public async Task<ResultService> CreateImageBase64Async(PersonImageDTO personImageDTO)
        {
            if (personImageDTO == null)
                return ResultService.Fail("Objeto de imagem deve ser informado");

            var validator = new PersonImageDTOValidator().Validate(personImageDTO);
            if (!validator.IsValid)
                return ResultService.RequestError("Problemas com a validação dos campos", validator);

            var person = await _personRepository.GetByIdAsync(personImageDTO.PersonId);
            if (person == null)
                return ResultService.Fail("Pessoa não encontrada");

            var personImage = new PersonImage(personImageDTO.PersonId, null, null, personImageDTO.Image);
            await _personImageRepository.CreateAsync(personImage);
            return ResultService.Ok("Imagem inserida no banco");
        }

        public async Task<ResultService> CreateImageUrlAsync(PersonImageDTO personImageDTO)
        {
            if (personImageDTO == null)
                return ResultService.Fail("Objeto de imagem deve ser informado");

            var validator = new PersonImageDTOValidator().Validate(personImageDTO);
            if (!validator.IsValid)
                return ResultService.RequestError("Problemas com a validação dos campos", validator);

            var person = await _personRepository.GetByIdAsync(personImageDTO.PersonId);
            if (person == null)
                return ResultService.Fail("Pessoa não encontrada");

            var imagePath = _savePersonImage.CreateImageUrl(personImageDTO.Image);
            var personImage = new PersonImage(personImageDTO.PersonId, imagePath, null, null);

            await _personImageRepository.CreateAsync(personImage);
            return ResultService.Ok("Imagem inserida no banco");
        }

        public async Task<ResultService> CreateImageUrlCloudinaryAsync(PersonImageDTO personImageDTO)
        {
            if (personImageDTO == null)
                return ResultService.Fail("Objeto de imagem deve ser informado");

            var validator = new PersonImageDTOValidator().Validate(personImageDTO);
            if (!validator.IsValid)
                return ResultService.RequestError("Problemas com a validação dos campos", validator);

            var person = await _personRepository.GetByIdAsync(personImageDTO.PersonId);
            if (person == null)
                return ResultService.Fail("Pessoa não encontrada");

            var imagePath = _savePersonImage.CreateImageUrlCloudinary(personImageDTO.Image, person.Name);
            var personImage = new PersonImage(personImageDTO.PersonId, null, imagePath, null);

            await _personImageRepository.CreateAsync(personImage);
            return ResultService.Ok("Imagem inserida no banco");
        }
    }
}
