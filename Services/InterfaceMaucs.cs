namespace DuAnTruongTim.Services;

public interface InterfaceMaucs
{
     public Task<dynamic> getAll();
    public Task<dynamic> getId(int id);
     public Task<dynamic> getDetailId(int id);
    public Task<dynamic> getIdTên_Data_phụ(int id );
    public Task<dynamic> getIdTên_Data_phụ1(int id);
     public Task<dynamic> getAllDetail() ;
}
