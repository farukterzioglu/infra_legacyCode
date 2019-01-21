using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.CrossCutting;
using Infrastructure.Common.DAL;
using Infrastructure.DAL;
using Infrastructure.Domain.CrossCutting;

namespace Infrastructure.DAL.EF
{
    //TODO : Unit test this 
    public class GenericRepositoryEFMappedExecuted<TDto, TEntity> where TDto : class, new() where TEntity : class, new()
    {
        public static void Execute(Action<IRepository<TDto>> execution, DbContext dbContext, IMapper mapper)
        {
            IRepository<TDto> repo = new GenericRepositoryEFMapped<TDto, TEntity>(dbContext, mapper);
            execution(repo);
            repo.Save();
        }
    }

    //TODO : Unit test
    public class GenericRepositoryEFMapped<TDto, TEntity> : 
        GenericRepositoryBase<TDto>, 
        IRepository<TDto> where TDto : class, new() where TEntity : class, new()
    {
        private readonly IRepository<TEntity> _proxyRepository;
        private readonly Infrastructure.Domain.CrossCutting.IMapper _mapper;

        public GenericRepositoryEFMapped(DbContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _proxyRepository = new GenericRepositoryEF<TEntity>(dbContext);
        }

        protected override TDto OnAdd(TDto entity)
        {
            var dbEntity = _mapper.Map<TDto, TEntity>(entity);
            TEntity newRecord = _proxyRepository.Add(dbEntity);

            return _mapper.Map<TEntity, TDto>(newRecord);
        }

        protected override void OnDelete(TDto entity)
        {
            _proxyRepository.Delete(_mapper.Map<TDto, TEntity>(entity));
        }

        protected override void OnDeleteById(object id)
        {
            _proxyRepository.DeleteById(id);
        }

        protected override void OnEdit(TDto entity)
        {
            TEntity temp = new TEntity();
            var mapped = _mapper.Map(entity, temp);

            _proxyRepository.Edit(mapped);
        }

        protected override IEnumerable<TDto> OnGetAll()
        {
            return _proxyRepository.GetAll().Select(x => _mapper.Map<TEntity, TDto>(x)).ToList();
        }
        
        protected override void OnAddRange(IEnumerable<TDto> obj)
        {
            _proxyRepository.AddRange(obj.Select(x => _mapper.Map<TDto, TEntity>(x)).ToList());
        }

        protected override int OnSave()
        {
            return _proxyRepository.Save();
        }

        protected override TDto OnGetById(object id)
        {
            var entity = _proxyRepository.GetById(id);
            return _mapper.Map<TEntity, TDto>(entity);
        }
    }
}
