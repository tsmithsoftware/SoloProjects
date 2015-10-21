SCHTASKS /Create /TN "Aargh Example Batch Task" /TR InstallingCronTask.exe /SC DAILY /st 16:10:00 /ru "SYSTEM"
pause
