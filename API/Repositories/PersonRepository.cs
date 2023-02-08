using API.Data;
using API.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly APIEntities _context;
        private readonly IMapper _mapper;

        public PersonRepository(APIEntities context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> AddAsync(PersonModel person)
        {
            try
            {
                await _context.AddAsync(_mapper.Map<Person>(person));
                _context.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
            }


        }

        public async Task<int> DeleteAsync(int id)
        {
            var person = await _context.Persons.SingleOrDefaultAsync(x => x.Id == id);

            if (person == null)
            {
                return 0;
            }


            var a = _mapper.Map<Person>(person);
            _context.Persons.Remove(a);
            await _context.SaveChangesAsync();

            return 1;


        }

        public async Task<List<PersonModel>> GetAllAsync()
        {
            var data = await _context.Persons.ToListAsync();

            return _mapper.Map<List<PersonModel>>(data);
        }

        public async Task<PersonModel> GetByIdAsync(int id)
        {
            var data = await _context.Persons.SingleOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<PersonModel>(data);
        }

        public async Task<int> UpdateAsync(int id, PersonModel person)
        {
            var a = await _context.Persons.SingleOrDefaultAsync(x => x.Id == id);

            if (a == null)
            {
                return 0;
            }

            a.Name = person.Name;
            a.Age = person.Age;

            await _context.SaveChangesAsync();

            return 1;

        }
    }
}
