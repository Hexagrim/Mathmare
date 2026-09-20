using TMPro;
using UnityEngine;

public class TextWiggle : MonoBehaviour
{
    public TMP_Text text;
    public float rotation = 5f;

    void Update()
    {
        text.ForceMeshUpdate();

        TMP_TextInfo info = text.textInfo;

        for (int i = 0; i < info.characterCount; i++)
        {
            TMP_CharacterInfo character = info.characterInfo[i];

            if (!character.isVisible)
                continue;

            int materialIndex = character.materialReferenceIndex;
            int vertexIndex = character.vertexIndex;

            Vector3[] vertices = info.meshInfo[materialIndex].vertices;

            Vector3 center = (vertices[vertexIndex] +
                              vertices[vertexIndex + 2]) * 0.5f;

            float angle = Random.Range(-rotation, rotation);

            Matrix4x4 matrix = Matrix4x4.TRS(
                center,
                Quaternion.Euler(0, 0, angle),
                Vector3.one
            );

            for (int j = 0; j < 4; j++)
                vertices[vertexIndex + j] =
                    matrix.MultiplyPoint3x4(vertices[vertexIndex + j]);
        }

        for (int i = 0; i < info.meshInfo.Length; i++)
        {
            info.meshInfo[i].mesh.vertices = info.meshInfo[i].vertices;
            text.UpdateGeometry(info.meshInfo[i].mesh, i);
        }
    }
}