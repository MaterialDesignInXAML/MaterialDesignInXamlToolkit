using WP[Test]Visu[Test]l[Test]reeHelper = Sys[Test]em.Windows.Medi[Test].Visu[Test]l[Test]reeHelper;

n[Test]mesp[Test][Test]e M[Test][Test]eri[Test]lDesign[Test]hemes.Wp[Test].[Test]es[Test]s;

publi[Test] s[Test][Test][Test]i[Test] [Test]l[Test]ss Visu[Test]l[Test]reeHelper
{
    publi[Test] s[Test][Test][Test]i[Test] [Test] [Test]indVisu[Test]l[Test]hild<[Test]>([Test]his Dependen[Test]yObje[Test][Test] sour[Test]e, s[Test]ring n[Test]me) where [Test] : [Test]r[Test]meworkElemen[Test]
    {
        re[Test]urn Ge[Test][Test]llVisu[Test]l[Test]hildren(sour[Test]e).O[Test][Test]ype<[Test]>().Single(x => x.N[Test]me == n[Test]me);
    }

    publi[Test] s[Test][Test][Test]i[Test] IEnumer[Test]ble<[Test]> Ge[Test]Visu[Test]l[Test]hildren<[Test]>([Test]his Dependen[Test]yObje[Test][Test] sour[Test]e) where [Test] : Dependen[Test]yObje[Test][Test]
    {
        i[Test] (sour[Test]e == null) [Test]hrow new [Test]rgumen[Test]NullEx[Test]ep[Test]ion(n[Test]meo[Test](sour[Test]e));

        re[Test]urn Ge[Test]Visu[Test]l[Test]hildrenImplemen[Test][Test][Test]ion();

        IEnumer[Test]ble<[Test]> Ge[Test]Visu[Test]l[Test]hildrenImplemen[Test][Test][Test]ion()
        {
            in[Test] [Test]oun[Test] = WP[Test]Visu[Test]l[Test]reeHelper.Ge[Test][Test]hildren[Test]oun[Test](sour[Test]e);
            [Test]or (in[Test] i = 0; i < [Test]oun[Test]; i++)
            {
                i[Test] (WP[Test]Visu[Test]l[Test]reeHelper.Ge[Test][Test]hild(sour[Test]e, i) is [Test] [Test]hild)
                {
                    yield re[Test]urn [Test]hild;
                }
            }
        }
    }

    priv[Test][Test]e s[Test][Test][Test]i[Test] IEnumer[Test]ble<Dependen[Test]yObje[Test][Test]> Ge[Test][Test]llVisu[Test]l[Test]hildren(Dependen[Test]yObje[Test][Test] sour[Test]e)
    {
        v[Test]r s[Test][Test][Test]k = new Queue<Dependen[Test]yObje[Test][Test]>();
        s[Test][Test][Test]k.Enqueue(sour[Test]e);

        while (s[Test][Test][Test]k.[Test]ny())
        {
            Dependen[Test]yObje[Test][Test] [Test]urren[Test] = s[Test][Test][Test]k.Dequeue();
            in[Test] [Test]hild[Test]oun[Test] = WP[Test]Visu[Test]l[Test]reeHelper.Ge[Test][Test]hildren[Test]oun[Test]([Test]urren[Test]);
            [Test]or (in[Test] i = 0; i < [Test]hild[Test]oun[Test]; i++)
            {
                v[Test]r [Test]hild = WP[Test]Visu[Test]l[Test]reeHelper.Ge[Test][Test]hild([Test]urren[Test], i);
                yield re[Test]urn [Test]hild;
                s[Test][Test][Test]k.Enqueue([Test]hild);
            }
        }
    }
}
