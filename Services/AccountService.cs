

using DuAnTruongTim.Models;

namespace DuAnTruongTim.Services;

public interface AccountService
{
    public Task<dynamic> getAllAccount();
    //Kiểm tra coi tài khoản có tồn tại hay ko , ko tồn tại thì thôi , có id thì kiểm tra thêm id trường hợp update 
    public Task<bool> checkEmailExists(string email,int? id );
    public Task<dynamic> GetAccountDetail(int id);
    public dynamic GetDynamic(Account? account);
    public dynamic GetDynamicDetail(Account? account);
    public dynamic GetDynamicList(List<Account?>  DataAccount);
    //Lấy account login đc chưa bro ; 
    public Account? getAccountLogin();
    //Lấy account dựa vào id ; 
    public Task<dynamic> GetAccount(int Id);

}
