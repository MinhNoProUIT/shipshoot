using UnityEngine;
using UnityEngine.UI; // Nếu sử dụng Text của Unity
using TMPro; // Nếu sử dụng TextMeshPro

public class CountdownTimer : MonoBehaviour
{
    public int countdownTime = 300; // Thời gian đếm ngược (5 phút = 300 giây)
    public TextMeshProUGUI tmpText; // Sử dụng nếu bạn dùng TextMeshPro

    private int currentTime; // Biến lưu thời gian hiện tại
    public int CurrentTime => currentTime;

    private void Start()
    {
        currentTime = countdownTime; // Đặt thời gian ban đầu
        UpdateUITimer(); // Cập nhật UI ngay khi bắt đầu

        // Bắt đầu đếm ngược mỗi giây
        InvokeRepeating(nameof(Countdown), 1f, 1f);
    }

    private void Countdown()
    {
        if (currentTime > 0)
        {
            currentTime--; // Giảm thời gian
            UpdateUITimer(); // Cập nhật giao diện
        }
        else
        {
            CancelInvoke(nameof(Countdown)); // Dừng đếm ngược khi hết thời gian
            OnCountdownEnd(); // Gọi hàm xử lý khi hết giờ
        }
    }

    private void UpdateUITimer()
    {
        int minutes = currentTime / 60; // Lấy số phút
        int seconds = currentTime % 60; // Lấy số giây còn lại

        string timeString = $"{minutes:00}:{seconds:00}"; // Định dạng "MM:SS"

        // Cập nhật UI
        if (tmpText != null) tmpText.text = timeString;
    }

    private void OnCountdownEnd()
    {
        Debug.Log("Countdown ended!");
        // Thêm logic xử lý khi hết giờ (hiển thị UI, chuyển màn hình, v.v.)
    }
}
