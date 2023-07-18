

using Azure.Identity;
using DuAnTruongTim.Models;

namespace DuAnTruongTim.Services;

public interface AccountService
{
    public Task<dynamic> getAllAccount();


    public dynamic getAccount();



    public Task<bool> checkEmailExists(string email,int? id );
    public Task<dynamic> GetAccountDetail(int id);
    public dynamic GetDynamic(Account? account);
    public dynamic GetDynamicDetail(Account? account);
    public dynamic GetDynamicList(List<Account?>  DataAccount);
    //Lấy account login đc chưa bro ; 
    public Account? getAccountLogin();
    //Lấy account dựa vào id ; 
    public Task<dynamic> GetAccount(int Id);
     public Task<dynamic?> login(string username , string password);
    public Task<bool> changePass(string username , string password);

}
