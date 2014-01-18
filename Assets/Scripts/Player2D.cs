using UnityEngine;
using System.Collections;
public class Player2D : MonoBehaviour {
    public GameObject hitPrefab;
    Animator animator;
    GameObject enemy;
    float speed = 0f;
    bool isMove = false;
    void Start()
    {
        animator = GetComponent<Animator>();
        enemy = GameObject.Find("Enemy2D");
	}
    void OnDeal()
    {
        if (hitPrefab) Instantiate(hitPrefab, transform.position+Vector3.right*2.5f-Vector3.up*0.5f, Quaternion.identity);
        float dist = enemy.transform.position.x - transform.position.x;
        if (dist > 3f || dist < 1) return;
        enemy.SendMessage("Damage", SendMessageOptions.DontRequireReceiver);
    }
    void GotoState(string some)
    {
        //animator.SetTrigger(some);
        animator.Play(some);
    }
    void Move()
    {
        speed += 0.01f;
        if (speed > 1) speed = 1;
        animator.SetFloat("Speed", speed);
    }
    void Stop()
    {
        speed = 0f;
        animator.SetFloat("Speed", speed);
    }
    void OnGUI()
    {
        Event e = Event.current;
        Rect area = new Rect(10, 0, 90 * Screen.width / 600f, 100 * Screen.width / 800f);
        int gap = 5;
        float gridHeight = area.height + gap;

        GUI.BeginGroup(new Rect(Screen.width - area.width - gap, 0, area.width, Screen.height));
        if (GUI.Button(new Rect(0, Screen.height - gridHeight * 1, area.width, area.height), "Q:Attack1")) GotoState("Attack");
        if (GUI.Button(new Rect(0, Screen.height - gridHeight * 2, area.width, area.height), "W:Attack2")) GotoState("Attack2");
        if (GUI.Button(new Rect(0, Screen.height - gridHeight * 3, area.width, area.height), "E:Attack3")) GotoState("Attack3");
        GUI.EndGroup();

        Rect btArea = new Rect(gap, Screen.height - gridHeight * 1, area.width, area.height);
        if (btArea.Contains(e.mousePosition) && e.isMouse)
        {
            if (e.type == EventType.MouseDown) isMove = true;
            if (e.type == EventType.MouseUp) isMove = false;
        }
        GUI.BeginGroup(new Rect(gap, 0, area.width, Screen.height));
        GUI.Button(new Rect(0, Screen.height - gridHeight * 1, area.width, area.height), "Right:Go");
        if (GUI.Button(new Rect(0, Screen.height - gridHeight * 2, area.width, area.height), "Space:Jump")) GotoState("Jump");
        GUI.EndGroup();
    }
    void Update()
    {
        if (animator == null) return;
        if (Input.GetKey(KeyCode.Escape)) Application.Quit();
        if (Input.GetKeyDown(KeyCode.Q)) GotoState("Attack");
        if (Input.GetKeyDown(KeyCode.W)) GotoState("Attack2");
        if (Input.GetKeyDown(KeyCode.E)) GotoState("Attack3");
        if (Input.GetKeyDown(KeyCode.Space)) GotoState("Jump");
        if (Input.GetKeyDown(KeyCode.RightArrow)) isMove = true;
        if (Input.GetKeyUp(KeyCode.RightArrow)) isMove = false;
        if (isMove) Move();
        else Stop();
        if (speed > 0f)
            transform.position += Vector3.right * speed * 0.03f;
    }
}
