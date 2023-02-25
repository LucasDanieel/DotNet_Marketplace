
using AutoMapper;
using DotNet.Marketplace.Application.DTOs;
using DotNet.Marketplace.Application.DTOs.Validations;
using DotNet.Marketplace.Application.Services.Interfaces;
using DotNet.Marketplace.Domain.Entity;
using DotNet.Marketplace.Domain.FiltersDb;
using DotNet.Marketplace.Domain.Repository;

namespace DotNet.Marketplace.Application.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public PersonService(IPersonRepository personRepository, IMapper mapper)
        {
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public async Task<ResultService<ICollection<PersonDTO>>> GetAllAsync()
        {
            var peoples = await _personRepository.GetAlldAsync();

            return ResultService.Ok(_mapper.Map<ICollection<PersonDTO>>(peoples));

        }

        public async Task<ResultService<PersonDTO>> GetPersonByIdAsync(int id)
        {
            var person = await _personRepository.GetByIdAsync(id);
            if (person == null)
                return ResultService.Fail<PersonDTO>("Pessoa não encontrada");

            return ResultService.Ok(_mapper.Map<PersonDTO>(person));
        }

        public async Task<ResultService<PersonDTO>> CreateAsync(PersonDTO personDTO)
        {
            if(personDTO == null)
                return ResultService.Fail<PersonDTO>("Objeto deve ser informado");

            var validator = new PersonDTOValidator().Validate(personDTO);
            if (!validator.IsValid)
                return ResultService.RequestError<PersonDTO>("Problemas com a validação dos campos", validator);

            var person = _mapper.Map<Person>(personDTO);

            person = await _personRepository.CreateAsync(person);

            return ResultService.Ok(_mapper.Map<PersonDTO>(person));
        }

        public async Task<ResultService> UpdateAsync(PersonDTO personDTO)
        {
            if (personDTO == null)
                return ResultService.Fail<PersonDTO>("Objeto deve ser informado");

            var validator = new PersonDTOValidator().Validate(personDTO);
            if (!validator.IsValid)
                return ResultService.RequestError<PersonDTO>("Problemas com a validação dos campos", validator);

            var person = await _personRepository.GetByIdAsync(personDTO.Id);
            if (person == null)
                return ResultService.Fail<PersonDTO>("Pessoa não encontrada");

            person = _mapper.Map<PersonDTO, Person>(personDTO, person);

            await _personRepository.EditAsync(person);
            return ResultService.Ok($"Pessoa com o Id:{person.Id} atualizada");
        }

        public async Task<ResultService> DeleteAsync(int id)
        {
            var person = await _personRepository.GetByIdAsync(id);
            if (person == null)
                return ResultService.Fail<PersonDTO>("Pessoa não encontrada");

            await _personRepository.DeleteAsync(person);
            return ResultService.Ok($"Pessoa com o Id:{id} excluida com sucesso");
        }

        public async Task<ResultService<PagedBaseResponseDTO<PersonDTO>>> GetPagedPersonAsync(PersonFilterDb filterDb)
        {
            var peoples = await _personRepository.GetPagedAsync(filterDb);

            var result = new PagedBaseResponseDTO<PersonDTO>(_mapper.Map<List<PersonDTO>>(peoples.Data), peoples.TotalRegister);

            return ResultService.Ok(result);
        }
    }
}
