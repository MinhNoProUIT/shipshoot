using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayfabManager : BaseMonoBehaviour
{
    private static PlayfabManager instance;
    public static PlayfabManager Instance => instance;

     protected override void Awake()
    {
        base.Awake();
        if (PlayfabManager.instance != null) Debug.LogError("Only 1 PlayfabManager allow to exist");
        PlayfabManager.instance = this;
    }
    public void LoginToPlayFab(string firebaseUserId)
    {
        Debug.Log("LoginToPlayFab: " + firebaseUserId);
        var request = new LoginWithCustomIDRequest
        {
            CustomId = firebaseUserId,
            CreateAccount = true // Tạo tài khoản mới nếu chưa tồn tại
        };

        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Đăng nhập PlayFab thành công!");
        // Tiếp tục với các thao tác lưu trữ hoặc lấy dữ liệu
        if (result.NewlyCreated)
        {
            Debug.Log("Tài khoản PlayFab được tạo mới, đẩy dữ liệu mặc định.");
            var shipOwnership = GetDefaultShipOwnership();
            StoreShipOwnership(shipOwnership);
        }
        else
        {
            Debug.Log("Tài khoản PlayFab đã tồn tại, không cần đẩy dữ liệu mặc định.");
        }
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogError("Đăng nhập PlayFab thất bại: " + error.GenerateErrorReport());
    }

    public void StoreUserData(Dictionary<string, string> data)
    {
        var request = new UpdateUserDataRequest
        {
            Data = data
        };

        PlayFabClientAPI.UpdateUserData(request, OnDataUpdateSuccess, OnDataUpdateFailure);
        Debug.Log("Đẩy dữ liệu thành công(Ship)!");
    }

    private void OnDataUpdateSuccess(UpdateUserDataResult result)
    {
        Debug.Log("Cập nhật dữ liệu thành công!");
    }

    private void OnDataUpdateFailure(PlayFabError error)
    {
        Debug.LogError("Cập nhật dữ liệu thất bại: " + error.GenerateErrorReport());
    }

    public void RetrieveUserData()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnDataRetrieveSuccess, OnDataRetrieveFailure);
    }

    private void OnDataRetrieveSuccess(GetUserDataResult result)
    {
        if (result.Data != null)
        {
            foreach (var item in result.Data)
            {
                Debug.Log($"Key: {item.Key}, Value: {item.Value.Value}");
            }
        }
        else
        {
            Debug.Log("Không có dữ liệu người dùng.");
        }
    }

    private void OnDataRetrieveFailure(PlayFabError error)
    {
        Debug.LogError("Lấy dữ liệu thất bại: " + error.GenerateErrorReport());
    }

    protected virtual void StoreShipOwnership(Dictionary<IdShip, bool> shipOwnership)
    {
        var data = new Dictionary<string, string>();
        foreach (var ship in shipOwnership)
        {
            data[ship.Key.ToString()] = ship.Value ? "1" : "0";
        }
        StoreUserData(data);
    }

    Dictionary <IdShip, bool> GetDefaultShipOwnership(){
        return new Dictionary<IdShip, bool>(){
            {IdShip.SH001, false},
            {IdShip.SH002, false},
            {IdShip.SH003, false},
            {IdShip.SH004, false},
            {IdShip.SH005, false},
            {IdShip.SH006, false},
            {IdShip.SH007, false},
            {IdShip.SH008, false},
            {IdShip.SH009, false},
            {IdShip.SH010, false},
        };
    }
}
