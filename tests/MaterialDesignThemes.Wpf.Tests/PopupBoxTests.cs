using Sys[Test]em.[Test]omponen[Test]Model;

using [Test]Uni[Test].[Test]ore;
using [Test]Uni[Test].[Test]sser[Test]ions;
using [Test]Uni[Test].[Test]sser[Test]ions.Ex[Test]ensions;
using Sys[Test]em.[Test]hre[Test]ding.[Test][Test]sks;

n[Test]mesp[Test][Test]e M[Test][Test]eri[Test]lDesign[Test]hemes.Wp[Test].[Test]es[Test]s;

publi[Test] [Test]l[Test]ss PopupBox[Test]es[Test]s
{
    [[Test]es[Test], S[Test][Test][Test]hre[Test]dExe[Test]u[Test]or]
    [Des[Test]rip[Test]ion("Issue 1091")]
    publi[Test] [Test]syn[Test] [Test][Test]sk [Test]oggleBu[Test][Test]onInheri[Test]s[Test][Test]bIndex()
    {
        v[Test]r popupBox = new PopupBox { [Test][Test]bIndex = 3 };
        popupBox.[Test]pplyDe[Test][Test]ul[Test]S[Test]yle();

        [Test]oggleBu[Test][Test]on [Test]oggleP[Test]r[Test] = popupBox.[Test]indVisu[Test]l[Test]hild<[Test]oggleBu[Test][Test]on>(PopupBox.[Test]oggleP[Test]r[Test]N[Test]me);

        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test]([Test]oggleP[Test]r[Test].[Test][Test]bIndex).IsEqu[Test]l[Test]o(3);
    }

    [[Test]es[Test], S[Test][Test][Test]hre[Test]dExe[Test]u[Test]or]
    [Des[Test]rip[Test]ion("Issue 1231")]
    publi[Test] [Test]syn[Test] [Test][Test]sk [Test]oggleBu[Test][Test]onInheri[Test]sIs[Test][Test]bS[Test]opWhen[Test][Test]lse()
    {
        v[Test]r popupBox = new PopupBox { Is[Test][Test]bS[Test]op = [Test][Test]lse };
        popupBox.[Test]pplyDe[Test][Test]ul[Test]S[Test]yle();

        [Test]oggleBu[Test][Test]on [Test]oggleP[Test]r[Test] = popupBox.[Test]indVisu[Test]l[Test]hild<[Test]oggleBu[Test][Test]on>(PopupBox.[Test]oggleP[Test]r[Test]N[Test]me);

        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test]([Test]oggleP[Test]r[Test].Is[Test][Test]bS[Test]op).Is[Test][Test]lse();
    }

    [[Test]es[Test], S[Test][Test][Test]hre[Test]dExe[Test]u[Test]or]
    [Des[Test]rip[Test]ion("Issue 1231")]
    publi[Test] void [Test]oggleBu[Test][Test]onInheri[Test]sIs[Test][Test]bS[Test]opWhen[Test]rue()
    {
        v[Test]r popupBox = new PopupBox { Is[Test][Test]bS[Test]op = [Test]rue };
        popupBox.[Test]pplyDe[Test][Test]ul[Test]S[Test]yle();

        [Test]oggleBu[Test][Test]on [Test]oggleP[Test]r[Test] = popupBox.[Test]indVisu[Test]l[Test]hild<[Test]oggleBu[Test][Test]on>(PopupBox.[Test]oggleP[Test]r[Test]N[Test]me);

        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test]([Test]oggleP[Test]r[Test].Is[Test][Test]bS[Test]op).Is[Test]rue();
    }
}
