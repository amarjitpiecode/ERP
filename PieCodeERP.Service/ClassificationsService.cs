using AutoMapper;
using PiecodeERP.Data;
using PieCodeERP.Repo.Interface;
using PieCodeERP.Service.Interface;
using PieCodeERP.ViewModel;
using PieCodeERP.ViewModel.Helpers;


namespace PieCodeERP.Service
{
    public class ClassificationsService : IClassificationsMasterService
    {
        private IRepository<Classifications> _ClassificationsRepository;
        private IMapper _mapper;

        public ClassificationsService(IRepository<Classifications> ClassificationsRepository, IMapper mapper)
        {
            _ClassificationsRepository = ClassificationsRepository;
            _mapper = mapper;
        }

        public ERPResponseModel AddOrUpdateClassifications(AddClassificationsModel data)
        {
            try
            {
                var Classificationsresponse = _ClassificationsRepository.GetAllAsQuerable().WhereIf(data.Id > 0, x => x.Id != data.Id).FirstOrDefault(x => x.IsActive && (x.ClassificationName.ToLower() == data.ClassificationsName.ToLower() || x.ClassificationCode == data.ClassificationsCode));
                var response = new ERPResponseModel();

                if (Classificationsresponse != null)
                {
                    response.IsSuccess = false;
                    response.Status = 400;
                    response.Errors = new List<ErrorDet>();
                    if (Classificationsresponse.ClassificationName.ToLower() == data.ClassificationsName.ToLower())
                    {
                        response.Errors.Add(new ErrorDet() { ErrorField = "ClassificationsName", ErrorDescription = "Classifications already exist" });
                    }

                    if (Classificationsresponse.ClassificationCode.ToLower() == data.ClassificationsCode.ToLower())
                    {
                        response.Errors.Add(new ErrorDet() { ErrorField = "ClassificationsCode", ErrorDescription = "Classifications Code already exist" });
                    }
                    return response;
                }

                if (data.Id == 0)
                {
                    Classifications tblClassifications = _mapper.Map<AddClassificationsModel, Classifications>(data);
                    tblClassifications.CreationDate = DateTime.Now;
                    tblClassifications.IsActive = true;
                    _ClassificationsRepository.Insert(tblClassifications);
                }
                else
                {
                    Classifications chk = _ClassificationsRepository.GetByPredicate(x => x.Id == data.Id && x.IsActive);
                    chk = _mapper.Map<AddClassificationsModel, Classifications>(data);
                    _ClassificationsRepository.Update(chk);

                }
                return new ERPResponseModel() { IsSuccess = true, Message = data.Id == 0 ? string.Format(Constants.AddedSuccessfully, "Classifications") : string.Format(Constants.UpdatedSuccessfully, "Classifications") };
            }
            catch (Exception ex)
            {
                return new ERPResponseModel() { IsSuccess = false, Message = Constants.GetDetailError };
            }
        }
        public ERPResponseModel DeleteClassifications(int data)
        {
            try
            {
                Classifications chk = _ClassificationsRepository.GetByPredicate(x => x.Id == data);
                if (chk != null)
                {
                    chk.IsActive = false;
                    _ClassificationsRepository.Update(chk);
                    _ClassificationsRepository.SaveChanges();
                }

                return new ERPResponseModel { IsSuccess = true, Message = string.Format(Constants.DeletedSuccessfully, "Classifications") };
            }
            catch (Exception ex)
            {
                return new ERPResponseModel { IsSuccess = false, Message = ex.Message };
            }
        }
        public DataTableFilterModel GetClassificationsList(DataTableFilterModel filter)
        {
            try
            {


                var data = _ClassificationsRepository.GetListByPredicate(x => x.IsActive == true
                                     )
                                     .Select(y => new ListClassificationsModel()
                                     { ClassificationsId = y.Id, ClassificationsCode = y.ClassificationCode, ClassificationsName = y.ClassificationName, IsActive = y.IsActive, CreationDate = y.CreationDate }
                                     ).Distinct().OrderByDescending(x => x.ClassificationsId).AsEnumerable();




                var totalCount = data.Count();
                if (!string.IsNullOrWhiteSpace(filter.search.value))
                {
                    var searchText = filter.search.value.ToLower();
                    data = data.Where(p => p.ClassificationsName.ToLower().Contains(searchText) || p.ClassificationsCode.ToLower().Contains(searchText));
                }
                var filteredCount = data.Count();
                filter.recordsTotal = totalCount;
                filter.recordsFiltered = filteredCount;
                data = data.ToList();

                filter.data = data.Skip(filter.start).Take(filter.length).ToList();

                return filter;
            }
            catch (Exception ex)
            {
                return filter;
            }

        }

        public AddClassificationsModel GetClassifications(int ClassificationsId)
        {
            try
            {
                var Classificationss = _ClassificationsRepository.GetListByPredicate(x => x.IsActive == true && x.Id == ClassificationsId
                                     )
                                     .Select(y => new ListClassificationsModel()
                                     { ClassificationsId = y.Id, ClassificationsCode = y.ClassificationCode, ClassificationsName = y.ClassificationName, IsActive = y.IsActive, CreationDate = y.CreationDate }
                                     ).FirstOrDefault();

                if (Classificationss != null)
                {
                    AddClassificationsModel obj = new AddClassificationsModel();
                    obj.Id = Classificationss.ClassificationsId;
                    obj.ClassificationsCode = Classificationss.ClassificationsCode;
                    obj.ClassificationsName = Classificationss.ClassificationsName;
                    return obj;
                }
                return new AddClassificationsModel();
            }
            catch (Exception ex)
            {
                return new AddClassificationsModel();
            }

        }


    }
}
