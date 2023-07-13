namespace DuAnTruongTim.Services;

public interface AccountService
{
    public Task<dynamic> getAllAccount();
    //Kiểm tra coi tài khoản có tồn tại hay ko , ko tồn tại thì thôi , có id thì kiểm tra thêm id trường hợp update 
    public Task<bool> checkEmailExists(string email,int? id );
}
