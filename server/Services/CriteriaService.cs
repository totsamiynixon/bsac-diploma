using Data.Interfaces;
using Data.Interfaces.Repositories;
using Entity.Domain;
using Services.DTO.Criteria;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using AutoMapper;
using Z.EntityFramework.Plus;

namespace Services
{
    public class CriteriaService : ICriteriaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Criteria> _criteriaRepository;
        public CriteriaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _criteriaRepository = _unitOfWork.Repository<Criteria>();
        }
        public async Task<int> AddOrUpdateCriteriaAsync(CriteriaDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }
            var criteria = Mapper.Map<CriteriaDTO, Criteria>(dto);
            var criteriaInDb = await _criteriaRepository.CollectionWithTracking.FirstOrDefaultAsync(f => dto.Id == f.Id);
            if (criteriaInDb == null)
            {
                _criteriaRepository.Insert(criteria);
            }
            else
            {
                Mapper.Map(criteria, criteriaInDb);
                _criteriaRepository.Update(criteriaInDb);
            }
            await _unitOfWork.SaveChangesAsync();
            return criteria.Id;
        }

        public async Task DeleteAsync(params int[] ids)
        {
            await _criteriaRepository.Collection.Where(f => ids.Any(z => z == f.Id)).DeleteAsync();
        }

        public async Task<List<CriteriaDTO>> GetAllAsync()
        {
            return await _criteriaRepository.Collection.Select(f => new CriteriaDTO
            {
                Id = f.Id,
                Name = f.Name
            }).ToListAsync();
        }

        public async Task<CriteriaDTO> GetByIdAsync(int id)
        {
            return await _criteriaRepository.Collection.Where(f => f.Id == id).Select(f => new CriteriaDTO
            {
                Id = f.Id,
                Name = f.Name
            }).FirstOrDefaultAsync();
        }
    }
}
