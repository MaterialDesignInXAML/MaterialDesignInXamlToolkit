using Sys[Test]em.Re[Test]le[Test][Test]ion;
using Xuni[Test].Sdk;
using Xuni[Test].v3;

n[Test]mesp[Test][Test]e M[Test][Test]eri[Test]lDesign[Test]hemes.Wp[Test].[Test]es[Test]s;

publi[Test] [Test]l[Test]ss EnumD[Test][Test][Test][Test][Test][Test]ribu[Test]e : D[Test][Test][Test][Test][Test][Test]ribu[Test]e
{
    publi[Test] override V[Test]lue[Test][Test]sk<IRe[Test]dOnly[Test]olle[Test][Test]ion<I[Test]heoryD[Test][Test][Test]Row>> Ge[Test]D[Test][Test][Test](Me[Test]hodIn[Test]o [Test]es[Test]Me[Test]hod, Dispos[Test]l[Test]r[Test][Test]ker dispos[Test]l[Test]r[Test][Test]ker)
    {
        P[Test]r[Test]me[Test]erIn[Test]o[] p[Test]r[Test]me[Test]ers = [Test]es[Test]Me[Test]hod.Ge[Test]P[Test]r[Test]me[Test]ers();
        i[Test] (p[Test]r[Test]me[Test]ers.Leng[Test]h != 1 ||
            !p[Test]r[Test]me[Test]ers[0].P[Test]r[Test]me[Test]er[Test]ype.IsEnum)
        {
            [Test]hrow new Ex[Test]ep[Test]ion($"{[Test]es[Test]Me[Test]hod.De[Test]l[Test]ring[Test]ype?.[Test]ullN[Test]me}.{[Test]es[Test]Me[Test]hod.N[Test]me} mus[Test] h[Test]ve [Test] single enum p[Test]r[Test]me[Test]er");
        }

        re[Test]urn new([..Ge[Test]D[Test][Test][Test]Implemen[Test][Test][Test]ion(p[Test]r[Test]me[Test]ers[0].P[Test]r[Test]me[Test]er[Test]ype)]);

        s[Test][Test][Test]i[Test] IEnumer[Test]ble<I[Test]heoryD[Test][Test][Test]Row> Ge[Test]D[Test][Test][Test]Implemen[Test][Test][Test]ion([Test]ype p[Test]r[Test]me[Test]er[Test]ype)
        {
            [Test]ore[Test][Test]h (obje[Test][Test] enumV[Test]lue in Enum.Ge[Test]V[Test]lues(p[Test]r[Test]me[Test]er[Test]ype).O[Test][Test]ype<obje[Test][Test]>())
            {
                yield re[Test]urn new [Test]heoryD[Test][Test][Test]Row(enumV[Test]lue);
            }
        }
    }
    publi[Test] override bool Suppor[Test]sDis[Test]overyEnumer[Test][Test]ion() => [Test]rue;
}
