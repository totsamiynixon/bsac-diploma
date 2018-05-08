using AutoMapper;
using Data.Interfaces;
using Data.Interfaces.Repositories;
using Entity.Domain;
using Services.DTO.Profession;
using Services.DTO.ProfessionCriteria;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace Services
{
    public class ProfessionService : IProfessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Profession> _professionRepository;
        private readonly IGenericRepository<ProfessionCriteria> _professionCriteriaRepository;
        public ProfessionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _professionRepository = _unitOfWork.Repository<Profession>();
            _professionCriteriaRepository = _unitOfWork.Repository<ProfessionCriteria>();
        }
        public async Task<int> AddOrUpdateProfessionAsync(ProfessionDetailsDTO dto)
        {
            var profession = Mapper.Map<ProfessionDetailsDTO, Profession>(dto);
            var professionInDb = await _professionRepository.CollectionWithTracking.Include(z => z.ProfessionCriterias).FirstOrDefaultAsync(f => f.Id == profession.Id);
            if (professionInDb == null)
            {
                _professionRepository.Insert(profession);
            }
            else
            {
                Mapper.Map(profession, professionInDb);
                _professionCriteriaRepository.Delete(professionInDb.ProfessionCriterias.ToArray());
                professionInDb.ProfessionCriterias.AddRange(profession.ProfessionCriterias);
                _professionRepository.Update(professionInDb);
            }
            await _unitOfWork.SaveChangesAsync();
            return profession.Id;
        }

        public async Task DeleteAsync(params int[] ids)
        {
            await _professionRepository.Collection.Where(f => ids.Any(z => z == f.Id)).DeleteAsync();
        }

        public async Task<List<ProfessionDTO>> GetAllAsync()
        {
            return await _professionRepository.Collection.Select(f => new ProfessionDTO
            {
                Id = f.Id,
                Name = f.Name
            }).ToListAsync();
        }

        public async Task<ProfessionDetailsDTO> GetByIdAsync(int id)
        {
            var profession = await _professionRepository.Collection.Include(f => f.ProfessionCriterias).FirstOrDefaultAsync(f => f.Id == id);
            return Mapper.Map<Profession, ProfessionDetailsDTO>(profession);
        }
    }
}
