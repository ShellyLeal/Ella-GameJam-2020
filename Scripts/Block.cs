using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Block : MonoBehaviour
{

    public event System.Action<Block> OnBlockPressed;
    public event System.Action OnFinishedMoving;
    public Vector2Int coord;

    Vector2Int startingCoord;
    MeshRenderer selfMesh;
    bool isCorrect = false;

    // metodo para iniciar a criacao do bloco, peca do puzzle
    public void Init (Vector2Int startingCoord, Texture2D image)
    {
        selfMesh = GetComponent<MeshRenderer>();
        this.startingCoord = startingCoord;
        coord = startingCoord;
        selfMesh.material = Resources.Load<Material>("Block");
        selfMesh.material.mainTexture = image;
        IsAtStartingCoord();
    }

    //metodo para iniciar a rotina de movimentacao do bloco
    public void MoveToPosition(Vector2 target, float duration)
    {
        StartCoroutine(AnimateMove(target, duration));
    }

    //metodo para detectar que o jogador clicou para mover a peca
    void OnMouseDown()
    {
        if (!IsPointerOverUIObject())
        {
            OnBlockPressed?.Invoke(this);
        }
    }

    //metodo para criar a animacao do movimento da peca do puzzle
    IEnumerator AnimateMove(Vector2 target, float duration)
    {
        Vector2 initialPos = transform.position;
        float percent = 0;

        while (percent < 1)
        {
            percent += Time.deltaTime / duration;
            transform.position = Vector2.Lerp (initialPos, target, percent);
            yield return null;
        }
        IsAtStartingCoord();
        OnFinishedMoving?.Invoke();
    }

    // metodo para tornar a peca cinza quando nao esta no local de origem
    public IEnumerator AnimateGrayscale (float duration, bool isGrayScale)
    {
        float time = 0;
        while (duration > time)
        {
            float durationFrame = Time.deltaTime;
            float ratio = time / duration;
            float grayAmount = isGrayScale
                ? ratio
                : 1 - ratio;
            SetGrayscale(grayAmount);
            time += durationFrame;
            yield return null;
        }
        SetGrayscale(isGrayScale? 1:0);
    }

    
    private void SetGrayscale (float amount = 1)
    {
        selfMesh.material.SetFloat("_GrayscaleAmount", amount);
    }

    //metodo para verificar se o bloco esta na posicao correta
    public bool IsAtStartingCoord()
    {
        if (coord == startingCoord && gameObject.activeSelf)
        {
            if (!isCorrect)
            {
                StartCoroutine(AnimateGrayscale(.2f, false));
            }
            isCorrect = true;
        }
        else
        {
            if (isCorrect && gameObject.activeSelf)
            {
                StartCoroutine(AnimateGrayscale(.2f, true));
                isCorrect = false;
            }
        }
        return coord == startingCoord;

    }

    //metodo para verificar a posicao do bloco e do mouse
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}