using UnityEngine;
using UnityEditor;
using System.Diagnostics; // 프로그램을 실행하기 위해 필요합니다.
using System.IO;

public class GitHub : Editor
{
    [MenuItem("Git/GitHub")]
    public static void LaunchGitHubDesktop()
    {
        // 윈도우 사용자 계정 이름을 자동으로 가져와서 설치 경로를 만듭니다.
        string localAppData = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
        string path = Path.Combine(localAppData, @"GitHubDesktop\GitHubDesktop.exe");

        try
        {
            // 프로그램 실행 시도
            Process.Start(path);
        }
        catch (System.Exception e)
        {
            // 설치 경로가 다르거나 파일이 없을 경우 에러 출력
            UnityEngine.Debug.LogError("GitHub Desktop을 찾을 수 없습니다. 설치 경로를 확인하세요: " + e.Message);
        }
    }
}
public class Commit : EditorWindow
{
    private string commitMessage = "";
    private string selectedTag = "feat";
    private string remoteUrl = ""; // 깃허브 주소 입력 변수
    private readonly string[] tags = { "feat(새로운 기능)", "fix(버그 수정)", "design(그래픽)", "refactor(코드 정리)", "docs(주석)", "chore(기타 설정)" };

    [MenuItem("Git/Commit")]
    public static void ShowWindow()
    {
        //팝업 창 표시
        GetWindow<Commit>("Git Commit");
    }

    private void OnGUI()
    {
        GUILayout.Label("Get Commit Tool", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        // 1. 프로젝트 최상위 폴더 경로와 .git 폴더 경로 찾기
        string projectPath = Directory.GetParent(Application.dataPath).FullName;
        string gitFolderPath = Path.Combine(projectPath, ".git");
        string gitIgnorePath = Path.Combine(projectPath, ".gitignore");
        string configPath = Path.Combine(gitFolderPath, "config");

        // 2. .git 폴더가 없으면 초기화 버튼 보여주기
        if (!Directory.Exists(gitFolderPath))
        {
            GUI.backgroundColor = Color.red; // 경고색(빨강)
            GUILayout.Label("⚠ 현재 Git 저장소가 아닙니다.", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            if (GUILayout.Button("Initialize Repository (git init)", GUILayout.Height(40)))
            {
                RunGitCommand("init"); // git init 명령 실행
                UnityEngine.Debug.Log("Git 저장소가 성공적으로 생성되었습니다.");
            }
            GUI.backgroundColor = Color.white;
            return; // ★ 중요: 여기서 함수를 끝내서 아래 커밋 UI가 안 보이게 함
        }

        if (!File.Exists(gitIgnorePath))
        {
            GUILayout.Label("⚠ .gitignore 파일이 없습니다! (쓰레기 파일이 올라갑니다)", EditorStyles.boldLabel);
            GUILayout.Label("임시 파일(.vs, Library 등) 때문에 에러가 날 수 있습니다.\n아래 버튼을 눌러 해결하세요.", EditorStyles.helpBox);

            if (GUILayout.Button("2. .gitignore 생성 및 정리 (Create & Reset)", GUILayout.Height(40)))
            {
                CreateGitIgnore(gitIgnorePath);
                // 이미 잘못 add된 파일들이 있을 수 있으므로 reset으로 상태를 풀어줌
                RunGitCommand("reset");
                UnityEngine.Debug.Log(".gitignore 생성 및 스테이징 초기화 완료.");
            }
            return;
        }

        bool hasRemote = File.Exists(configPath) && File.ReadAllText(configPath).Contains("[remote \"origin\"]");
        if (!hasRemote)
        {
            GUILayout.Label("⚠ 깃허브와 연결되지 않았습니다.", EditorStyles.boldLabel);
            GUILayout.Label("GitHub Repository URL:");
            remoteUrl = EditorGUILayout.TextField(remoteUrl); // 주소 입력창

            if (GUILayout.Button("2. 깃허브 연결 (remote add)", GUILayout.Height(40)))
            {
                if (string.IsNullOrEmpty(remoteUrl))
                {
                    UnityEngine.Debug.LogError("URL을 입력하세요!");
                    return;
                }
                // 원격 저장소 연결 명령
                RunGitCommand($"remote add origin {remoteUrl}");
                UnityEngine.Debug.Log($"깃허브 연결 완료: {remoteUrl}");
                GUI.FocusControl(null);
            }

            GUILayout.Space(10);
            GUILayout.Label("Tip: 깃허브 웹사이트에서 'New Repository'를 만들고\n주소를 복사해서 붙여넣으세요.", EditorStyles.helpBox);
            return;
        }
        //컨벤션 태그 선택 버튼
        GUILayout.Label("Select Convention Tag:");
        int selectedIndex = System.Array.IndexOf(tags, selectedTag);
        selectedIndex = GUILayout.SelectionGrid(selectedIndex, tags, 3); //3열 배치
        if (selectedIndex != -1) selectedTag = tags[selectedIndex];

        EditorGUILayout.Space();

        // 메시지 입력 필드
        GUILayout.Label("Commit Message:");
        commitMessage = EditorGUILayout.TextField(commitMessage);

        EditorGUILayout.Space();

        // 최종 커밋 버튼
        GUI.backgroundColor = Color.green; // 버튼 색상 강조
        if (GUILayout.Button("Commit and Push", GUILayout.Height(40)))
        {
            if (string.IsNullOrEmpty(commitMessage))
            {
                UnityEngine.Debug.LogError("커밋 메시지를 입력해야 합니다.");
                return;
            }

            ExecuteGitSequence();
        }
        GUI.backgroundColor = Color.white;
    }

    private void CreateGitIgnore(string path)
    {
        // 유니티 표준 .gitignore 내용
        string content =
            "/[Ll]ibrary/\n/[Tt]emp/\n/[Oo]bj/\n/[Bb]uild/\n/[Bb]uilds/\n/[Ll]ogs/\n/[Uu]ser[Ss]ettings/\n" +
            ".vs/\n.idea/\n.vscode/\n*.csproj\n*.sln\n*.suo\n*.tmp\n*.user\n*.userprefs\n*.pidb\n*.booproj\n*.svd\n*.pdb\n*.opendb\n*.VC.db\n" +
            ".plastic/\n.utmp/\n.collabignore\n.DS_Store\nThumbs.db";

        File.WriteAllText(path, content);
    }

    private void ExecuteGitSequence()
    {
        // "feat(새로운 기능)"에서 앞에 있는 영어 태그만 추출 (괄호 앞까지)
        string pureTag = selectedTag.Split('(')[0];
        string finalMessage = $"{pureTag}: {commitMessage}";

        RunGitCommand("add .");
        RunGitCommand($"commit -m \"{finalMessage}\"");
        RunGitCommand("push origin master");

        UnityEngine.Debug.Log($"[Git Success] {finalMessage} 푸시 완료.");
        this.Close();
    }

    private static void RunGitCommand(string command)
    {
        ProcessStartInfo startInfo = new("git")
        {
            Arguments = command,
            WorkingDirectory = Application.dataPath + "/..",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using Process process = Process.Start(startInfo);

        // ★ 핵심: 15초(15000ms)까지만 기다려준다.
        // 그 이상 걸리면 "로그인 대기 중"으로 판단하고 끊어버린다.
        bool isFinished = process.WaitForExit(15000);

        if (!isFinished)
        {
            // 1. 프로세스 강제 종료
            process.Kill();

            // 2. 사용자에게 알림 (유니티 멈춤 방지)
            UnityEngine.Debug.LogError("⛔ Git 응답 시간 초과 (Timeout)!");
            bool openDesktop = EditorUtility.DisplayDialog(
                "Git 로그인 필요",
                "처음 사용할 때는 깃허브 로그인이 필요해서 시간이 오래 걸릴 수 있습니다.\n\n안전을 위해 작업을 취소했습니다.\nGitHub Desktop을 열어서 로그인을 완료해주세요.",
                "GitHub Desktop 열기", "닫기");

            // 3. 바로 해결할 수 있게 데스크톱 열어주기
            if (openDesktop)
            {
                GitHub.LaunchGitHubDesktop();
            }
            return;
        }

        // 정상 종료되었을 때만 로그 출력
        string output = process.StandardOutput.ReadToEnd();
        string error = process.StandardError.ReadToEnd();
        if (!string.IsNullOrEmpty(output)) UnityEngine.Debug.Log("Git: " + output);
        if (!string.IsNullOrEmpty(error) && !error.Contains("warning")) UnityEngine.Debug.LogWarning("Git Status: " + error);
    }
}