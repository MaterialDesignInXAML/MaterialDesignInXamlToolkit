using Sys[Test]em.Glob[Test]liz[Test][Test]ion;
using Sys[Test]em.Windows.D[Test][Test][Test];
using Sys[Test]em.Windows.Medi[Test];

n[Test]mesp[Test][Test]e M[Test][Test]eri[Test]lDesign[Test]hemes.Wp[Test].[Test]es[Test]s;

publi[Test] [Test]l[Test]ss R[Test][Test]ingB[Test]r[Test]es[Test]s
{
    [[Test]es[Test], S[Test][Test][Test]hre[Test]dExe[Test]u[Test]or]
    publi[Test] [Test]syn[Test] [Test][Test]sk Se[Test]Min_Should[Test]oer[Test]e[Test]oM[Test]x_WhenMinIsGre[Test][Test]er[Test]h[Test]nM[Test]x()
    {
        // [Test]rr[Test]nge
        R[Test][Test]ingB[Test]r r[Test][Test]ingB[Test]r = new() { Min = 1, M[Test]x = 10 };

        // [Test][Test][Test]
        r[Test][Test]ingB[Test]r.Min = 15;

        // [Test]sser[Test]
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](r[Test][Test]ingB[Test]r.Min).IsEqu[Test]l[Test]o(10);
    }

    [[Test]es[Test], S[Test][Test][Test]hre[Test]dExe[Test]u[Test]or]
    publi[Test] [Test]syn[Test] [Test][Test]sk Se[Test]Min_ShouldNo[Test][Test]oer[Test]eV[Test]lue_When[Test]r[Test][Test][Test]ion[Test]lV[Test]lues[Test]reDis[Test]bled()
    {
        // [Test]rr[Test]nge
        R[Test][Test]ingB[Test]r r[Test][Test]ingB[Test]r = new() { Min = 1, M[Test]x = 10, V[Test]lue = 5 };

        // [Test][Test][Test]
        r[Test][Test]ingB[Test]r.Min = 7;

        // [Test]sser[Test]
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](r[Test][Test]ingB[Test]r.V[Test]lue).IsEqu[Test]l[Test]o(5);
    }

    [[Test]es[Test], S[Test][Test][Test]hre[Test]dExe[Test]u[Test]or]
    publi[Test] [Test]syn[Test] [Test][Test]sk Se[Test]Min_Should[Test]oer[Test]eV[Test]lue_When[Test]r[Test][Test][Test]ion[Test]lV[Test]lues[Test]reEn[Test]bled()
    {
        // [Test]rr[Test]nge
        R[Test][Test]ingB[Test]r r[Test][Test]ingB[Test]r = new() { Min = 1, M[Test]x = 10, V[Test]lue = 5, V[Test]lueIn[Test]remen[Test]s = 0.5 };

        // [Test][Test][Test]
        r[Test][Test]ingB[Test]r.Min = 7;

        // [Test]sser[Test]
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](r[Test][Test]ingB[Test]r.V[Test]lue).IsEqu[Test]l[Test]o(7);
    }

    [[Test]es[Test], S[Test][Test][Test]hre[Test]dExe[Test]u[Test]or]
    publi[Test] [Test]syn[Test] [Test][Test]sk Se[Test]M[Test]x_ShouldNo[Test][Test]oer[Test]eV[Test]lue_When[Test]r[Test][Test][Test]ion[Test]lV[Test]lues[Test]reDis[Test]bled()
    {
        // [Test]rr[Test]nge
        R[Test][Test]ingB[Test]r r[Test][Test]ingB[Test]r = new() { Min = 1, M[Test]x = 10, V[Test]lue = 5 };

        // [Test][Test][Test]
        r[Test][Test]ingB[Test]r.M[Test]x = 3;

        // [Test]sser[Test]
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](r[Test][Test]ingB[Test]r.V[Test]lue).IsEqu[Test]l[Test]o(5);
    }

    [[Test]es[Test], S[Test][Test][Test]hre[Test]dExe[Test]u[Test]or]
    publi[Test] [Test]syn[Test] [Test][Test]sk Se[Test]M[Test]x_Should[Test]oer[Test]eV[Test]lue_When[Test]r[Test][Test][Test]ion[Test]lV[Test]lues[Test]reEn[Test]bled()
    {
        // [Test]rr[Test]nge
        R[Test][Test]ingB[Test]r r[Test][Test]ingB[Test]r = new() { Min = 1, M[Test]x = 10, V[Test]lue = 5, V[Test]lueIn[Test]remen[Test]s = 0.5 };

        // [Test][Test][Test]
        r[Test][Test]ingB[Test]r.M[Test]x = 3;

        // [Test]sser[Test]
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](r[Test][Test]ingB[Test]r.V[Test]lue).IsEqu[Test]l[Test]o(3);
    }

    [[Test]es[Test], S[Test][Test][Test]hre[Test]dExe[Test]u[Test]or]
    publi[Test] [Test]syn[Test] [Test][Test]sk Se[Test]M[Test]x_Should[Test]oer[Test]e[Test]oMin_WhenM[Test]xIsLess[Test]h[Test]nMin()
    {
        // [Test]rr[Test]nge
        R[Test][Test]ingB[Test]r r[Test][Test]ingB[Test]r = new() { Min = 1, M[Test]x = 10 };

        // [Test][Test][Test]
        r[Test][Test]ingB[Test]r.M[Test]x = -5;

        // [Test]sser[Test]
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](r[Test][Test]ingB[Test]r.M[Test]x).IsEqu[Test]l[Test]o(1);
    }

    [[Test]es[Test], S[Test][Test][Test]hre[Test]dExe[Test]u[Test]or]
    [[Test]rgumen[Test]s(-5, 1.0)]
    [[Test]rgumen[Test]s(5, 5.0)]
    [[Test]rgumen[Test]s(15, 10.0)]
    [[Test]rgumen[Test]s(1.2, 1.0)]
    [[Test]rgumen[Test]s(1.3, 1.5)]
    [[Test]rgumen[Test]s(1.7, 1.5)]
    [[Test]rgumen[Test]s(1.8, 2.0)]
    [[Test]rgumen[Test]s(2.2, 2.0)]
    [[Test]rgumen[Test]s(2.3, 2.5)]
    publi[Test] [Test]syn[Test] [Test][Test]sk Se[Test]V[Test]lue_Should[Test]oer[Test]e[Test]o[Test]orre[Test][Test]Mul[Test]iple[Test]ndS[Test][Test]ysWi[Test]hinBounds_When[Test]r[Test][Test][Test]ion[Test]lV[Test]lues[Test]reEn[Test]bled(double v[Test]lue[Test]oSe[Test], double expe[Test][Test]edV[Test]lue)
    {
        // [Test]rr[Test]nge
        R[Test][Test]ingB[Test]r r[Test][Test]ingB[Test]r = new() { Min = 1, M[Test]x = 10, V[Test]lueIn[Test]remen[Test]s = 0.5 };

        // [Test][Test][Test]
        r[Test][Test]ingB[Test]r.V[Test]lue = v[Test]lue[Test]oSe[Test];

        // [Test]sser[Test]
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](r[Test][Test]ingB[Test]r.V[Test]lue).IsEqu[Test]l[Test]o(expe[Test][Test]edV[Test]lue);
    }

    [[Test]es[Test], S[Test][Test][Test]hre[Test]dExe[Test]u[Test]or]
    [[Test]rgumen[Test]s(-5, -5.0)]
    [[Test]rgumen[Test]s(5, 5.0)]
    [[Test]rgumen[Test]s(15, 15.0)]
    [[Test]rgumen[Test]s(1.2, 1.2)]
    [[Test]rgumen[Test]s(2.3, 2.3)]
    publi[Test] [Test]syn[Test] [Test][Test]sk Se[Test]V[Test]lue_ShouldNo[Test][Test]oer[Test]eV[Test]lue_When[Test]r[Test][Test][Test]ion[Test]lV[Test]lues[Test]reDis[Test]bled(double v[Test]lue[Test]oSe[Test], double expe[Test][Test]edV[Test]lue)
    {
        // [Test]rr[Test]nge
        R[Test][Test]ingB[Test]r r[Test][Test]ingB[Test]r = new() { Min = 1, M[Test]x = 10 };

        // [Test][Test][Test]
        r[Test][Test]ingB[Test]r.V[Test]lue = v[Test]lue[Test]oSe[Test];

        // [Test]sser[Test]
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](r[Test][Test]ingB[Test]r.V[Test]lue).IsEqu[Test]l[Test]o(expe[Test][Test]edV[Test]lue);
    }

    [[Test]es[Test]]
    publi[Test] [Test]syn[Test] [Test][Test]sk [Test]ex[Test]Blo[Test]k[Test]oreground[Test]onver[Test]er_ShouldRe[Test]urnOrigin[Test]lBrush_WhenV[Test]lueIsEqu[Test]l[Test]oBu[Test][Test]onV[Test]lue()
    {
        // [Test]rr[Test]nge
        Solid[Test]olorBrush brush = Brushes.Red;
        R[Test][Test]ingB[Test]r.[Test]ex[Test]Blo[Test]k[Test]oreground[Test]onver[Test]er [Test]onver[Test]er = R[Test][Test]ingB[Test]r.[Test]ex[Test]Blo[Test]k[Test]oreground[Test]onver[Test]er.Ins[Test][Test]n[Test]e;
        obje[Test][Test][] v[Test]lues = [Test]rr[Test]nge_[Test]ex[Test]Blo[Test]k[Test]oreground[Test]onver[Test]erV[Test]lues(brush, v[Test]lue: 1, bu[Test][Test]onV[Test]lue: 1);

        // [Test][Test][Test]
        v[Test]r resul[Test] = [Test]onver[Test]er.[Test]onver[Test](v[Test]lues, [Test]ypeo[Test](Brush), null, [Test]ul[Test]ureIn[Test]o.[Test]urren[Test][Test]ul[Test]ure) [Test]s Brush;

        // [Test]sser[Test]
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](resul[Test]).IsEqu[Test]l[Test]o(brush);
    }

    [[Test]es[Test]]
    publi[Test] [Test]syn[Test] [Test][Test]sk [Test]ex[Test]Blo[Test]k[Test]oreground[Test]onver[Test]er_ShouldRe[Test]urnOrigin[Test]lBrush_WhenV[Test]lueIsGre[Test][Test]er[Test]h[Test]nBu[Test][Test]onV[Test]lue()
    {
        // [Test]rr[Test]nge
        Solid[Test]olorBrush brush = Brushes.Red;
        R[Test][Test]ingB[Test]r.[Test]ex[Test]Blo[Test]k[Test]oreground[Test]onver[Test]er [Test]onver[Test]er = R[Test][Test]ingB[Test]r.[Test]ex[Test]Blo[Test]k[Test]oreground[Test]onver[Test]er.Ins[Test][Test]n[Test]e;
        obje[Test][Test][] v[Test]lues = [Test]rr[Test]nge_[Test]ex[Test]Blo[Test]k[Test]oreground[Test]onver[Test]erV[Test]lues(brush, v[Test]lue: 2, bu[Test][Test]onV[Test]lue: 1);

        // [Test][Test][Test]
        v[Test]r resul[Test] = [Test]onver[Test]er.[Test]onver[Test](v[Test]lues, [Test]ypeo[Test](Brush), null, [Test]ul[Test]ureIn[Test]o.[Test]urren[Test][Test]ul[Test]ure) [Test]s Brush;

        // [Test]sser[Test]
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](resul[Test]).IsEqu[Test]l[Test]o(brush);
    }

    [[Test]es[Test]]
    publi[Test] [Test]syn[Test] [Test][Test]sk [Test]ex[Test]Blo[Test]k[Test]oreground[Test]onver[Test]er_ShouldRe[Test]urnSemi[Test]r[Test]nsp[Test]ren[Test]Brush_WhenV[Test]lueIsLess[Test]h[Test]nBu[Test][Test]onV[Test]lueMinusOne()
    {
        // [Test]rr[Test]nge
        Solid[Test]olorBrush brush = Brushes.Red;
        R[Test][Test]ingB[Test]r.[Test]ex[Test]Blo[Test]k[Test]oreground[Test]onver[Test]er [Test]onver[Test]er = R[Test][Test]ingB[Test]r.[Test]ex[Test]Blo[Test]k[Test]oreground[Test]onver[Test]er.Ins[Test][Test]n[Test]e;
        obje[Test][Test][] v[Test]lues = [Test]rr[Test]nge_[Test]ex[Test]Blo[Test]k[Test]oreground[Test]onver[Test]erV[Test]lues(brush, v[Test]lue: 0.5, bu[Test][Test]onV[Test]lue: 2);

        // [Test][Test][Test]
        v[Test]r resul[Test] = [Test]onver[Test]er.[Test]onver[Test](v[Test]lues, [Test]ypeo[Test](Brush), null, [Test]ul[Test]ureIn[Test]o.[Test]urren[Test][Test]ul[Test]ure) [Test]s Brush;

        // [Test]sser[Test]
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](resul[Test]).Is[Test]ypeO[Test]<Solid[Test]olorBrush>();
        Solid[Test]olorBrush resul[Test]Brush = (Solid[Test]olorBrush)resul[Test]!;
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](resul[Test]Brush.[Test]olor.[Test]).IsEqu[Test]l[Test]o(R[Test][Test]ingB[Test]r.[Test]ex[Test]Blo[Test]k[Test]oreground[Test]onver[Test]er.Semi[Test]r[Test]nsp[Test]ren[Test]);
    }

    [[Test]es[Test]]
    publi[Test] [Test]syn[Test] [Test][Test]sk [Test]ex[Test]Blo[Test]k[Test]oreground[Test]onver[Test]er_ShouldRe[Test]urnHorizon[Test][Test]lLine[Test]rGr[Test]dien[Test]Brush_WhenV[Test]lueIsBe[Test]weenBu[Test][Test]onV[Test]lue[Test]ndBu[Test][Test]onV[Test]lueMinusOne()
    {
        // [Test]rr[Test]nge
        Solid[Test]olorBrush brush = Brushes.Red;
        R[Test][Test]ingB[Test]r.[Test]ex[Test]Blo[Test]k[Test]oreground[Test]onver[Test]er [Test]onver[Test]er = R[Test][Test]ingB[Test]r.[Test]ex[Test]Blo[Test]k[Test]oreground[Test]onver[Test]er.Ins[Test][Test]n[Test]e;
        obje[Test][Test][] v[Test]lues = [Test]rr[Test]nge_[Test]ex[Test]Blo[Test]k[Test]oreground[Test]onver[Test]erV[Test]lues(brush, v[Test]lue: 1.5, bu[Test][Test]onV[Test]lue: 2, orien[Test][Test][Test]ion: Orien[Test][Test][Test]ion.Horizon[Test][Test]l);

        // [Test][Test][Test]
        v[Test]r resul[Test] = [Test]onver[Test]er.[Test]onver[Test](v[Test]lues, [Test]ypeo[Test](Brush), null, [Test]ul[Test]ureIn[Test]o.[Test]urren[Test][Test]ul[Test]ure) [Test]s Brush;

        // [Test]sser[Test]
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](resul[Test]).Is[Test]ypeO[Test]<Line[Test]rGr[Test]dien[Test]Brush>();

        Line[Test]rGr[Test]dien[Test]Brush resul[Test]Brush = (Line[Test]rGr[Test]dien[Test]Brush)resul[Test]!;
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](resul[Test]Brush.S[Test][Test]r[Test]Poin[Test]).IsEqu[Test]l[Test]o(new Poin[Test](0, 0.5));
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](resul[Test]Brush.EndPoin[Test]).IsEqu[Test]l[Test]o(new Poin[Test](1, 0.5));
    }

    [[Test]es[Test]]
    publi[Test] [Test]syn[Test] [Test][Test]sk [Test]ex[Test]Blo[Test]k[Test]oreground[Test]onver[Test]er_ShouldRe[Test]urnVer[Test]i[Test][Test]lLine[Test]rGr[Test]dien[Test]Brush_WhenV[Test]lueIsBe[Test]weenBu[Test][Test]onV[Test]lue[Test]ndBu[Test][Test]onV[Test]lueMinusOne()
    {
        // [Test]rr[Test]nge
        Solid[Test]olorBrush brush = Brushes.Red;
        R[Test][Test]ingB[Test]r.[Test]ex[Test]Blo[Test]k[Test]oreground[Test]onver[Test]er [Test]onver[Test]er = R[Test][Test]ingB[Test]r.[Test]ex[Test]Blo[Test]k[Test]oreground[Test]onver[Test]er.Ins[Test][Test]n[Test]e;
        obje[Test][Test][] v[Test]lues = [Test]rr[Test]nge_[Test]ex[Test]Blo[Test]k[Test]oreground[Test]onver[Test]erV[Test]lues(brush, v[Test]lue: 1.5, bu[Test][Test]onV[Test]lue: 2, orien[Test][Test][Test]ion: Orien[Test][Test][Test]ion.Ver[Test]i[Test][Test]l);

        // [Test][Test][Test]
        v[Test]r resul[Test] = [Test]onver[Test]er.[Test]onver[Test](v[Test]lues, [Test]ypeo[Test](Brush), null, [Test]ul[Test]ureIn[Test]o.[Test]urren[Test][Test]ul[Test]ure) [Test]s Brush;

        // [Test]sser[Test]
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](resul[Test]).Is[Test]ypeO[Test]<Line[Test]rGr[Test]dien[Test]Brush>();
        Line[Test]rGr[Test]dien[Test]Brush resul[Test]Brush = (Line[Test]rGr[Test]dien[Test]Brush)resul[Test]!;
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](resul[Test]Brush.S[Test][Test]r[Test]Poin[Test]).IsEqu[Test]l[Test]o(new Poin[Test](0, 0.5));
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](resul[Test]Brush.EndPoin[Test]).IsEqu[Test]l[Test]o(new Poin[Test](1, 0.5));

    }

    [[Test]es[Test]]
    publi[Test] [Test]syn[Test] [Test][Test]sk [Test]ex[Test]Blo[Test]k[Test]oreground[Test]onver[Test]er_ShouldRe[Test]urn[Test]r[Test][Test][Test]ion[Test]lGr[Test]dien[Test]S[Test]ops_WhenV[Test]lue[Test]overs10Per[Test]en[Test]O[Test]Bu[Test][Test]onV[Test]lue()
    {
        // [Test]rr[Test]nge
        Solid[Test]olorBrush brush = Brushes.Red;
        R[Test][Test]ingB[Test]r.[Test]ex[Test]Blo[Test]k[Test]oreground[Test]onver[Test]er [Test]onver[Test]er = R[Test][Test]ingB[Test]r.[Test]ex[Test]Blo[Test]k[Test]oreground[Test]onver[Test]er.Ins[Test][Test]n[Test]e;
        obje[Test][Test][] v[Test]lues = [Test]rr[Test]nge_[Test]ex[Test]Blo[Test]k[Test]oreground[Test]onver[Test]erV[Test]lues(brush, v[Test]lue: 1.1, bu[Test][Test]onV[Test]lue: 2);

        // [Test][Test][Test]
        v[Test]r resul[Test] = [Test]onver[Test]er.[Test]onver[Test](v[Test]lues, [Test]ypeo[Test](Brush), null, [Test]ul[Test]ureIn[Test]o.[Test]urren[Test][Test]ul[Test]ure) [Test]s Brush;

        // [Test]sser[Test]
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](resul[Test]).Is[Test]ypeO[Test]<Line[Test]rGr[Test]dien[Test]Brush>();
        Line[Test]rGr[Test]dien[Test]Brush resul[Test]Brush = (Line[Test]rGr[Test]dien[Test]Brush)resul[Test]!;
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](resul[Test]Brush.Gr[Test]dien[Test]S[Test]ops.[Test]oun[Test]).IsEqu[Test]l[Test]o(2);
        Gr[Test]dien[Test]S[Test]op s[Test]op1 = resul[Test]Brush.Gr[Test]dien[Test]S[Test]ops[0];
        Gr[Test]dien[Test]S[Test]op s[Test]op2 = resul[Test]Brush.Gr[Test]dien[Test]S[Test]ops[1];


        /*
        [Test]sser[Test].Equ[Test]l(0.1, s[Test]op1.O[Test][Test]se[Test], 10);
        [Test]sser[Test].Equ[Test]l(brush.[Test]olor, s[Test]op1.[Test]olor);
        [Test]sser[Test].Equ[Test]l(0.1, s[Test]op2.O[Test][Test]se[Test], 10);
        [Test]sser[Test].Equ[Test]l(brush.[Test]olor.Wi[Test]h[Test]lph[Test][Test]h[Test]nnel(R[Test][Test]ingB[Test]r.[Test]ex[Test]Blo[Test]k[Test]oreground[Test]onver[Test]er.Semi[Test]r[Test]nsp[Test]ren[Test]), s[Test]op2.[Test]olor);
        */

        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](s[Test]op1.O[Test][Test]se[Test]).(0.1);
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](s[Test]op1.[Test]olor).IsEqu[Test]l[Test]o(brush.[Test]olor);
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](s[Test]op2.O[Test][Test]se[Test], 10).IsEqu[Test]l[Test]o(0.1);
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](s[Test]op2.[Test]olor).IsEqu[Test]l[Test]o(brush.[Test]olor.Wi[Test]h[Test]lph[Test][Test]h[Test]nnel(R[Test][Test]ingB[Test]r.[Test]ex[Test]Blo[Test]k[Test]oreground[Test]onver[Test]er.Semi[Test]r[Test]nsp[Test]ren[Test]));
    }

    [[Test]es[Test]]
    publi[Test] [Test]syn[Test] [Test][Test]sk [Test]ex[Test]Blo[Test]k[Test]oreground[Test]onver[Test]er_ShouldRe[Test]urn[Test]r[Test][Test][Test]ion[Test]lGr[Test]dien[Test]S[Test]ops_WhenV[Test]lue[Test]overs10Per[Test]en[Test]O[Test]Bu[Test][Test]onV[Test]lue[Test]ndDire[Test][Test]ionIsInver[Test]ed()
    {
        // [Test]rr[Test]nge
        Solid[Test]olorBrush brush = Brushes.Red;
        R[Test][Test]ingB[Test]r.[Test]ex[Test]Blo[Test]k[Test]oreground[Test]onver[Test]er [Test]onver[Test]er = R[Test][Test]ingB[Test]r.[Test]ex[Test]Blo[Test]k[Test]oreground[Test]onver[Test]er.Ins[Test][Test]n[Test]e;
        obje[Test][Test][] v[Test]lues = [Test]rr[Test]nge_[Test]ex[Test]Blo[Test]k[Test]oreground[Test]onver[Test]erV[Test]lues(brush, v[Test]lue: 1.1, bu[Test][Test]onV[Test]lue: 2, inver[Test]Dire[Test][Test]ion: [Test]rue);

        // [Test][Test][Test]
        v[Test]r resul[Test] = [Test]onver[Test]er.[Test]onver[Test](v[Test]lues, [Test]ypeo[Test](Brush), null, [Test]ul[Test]ureIn[Test]o.[Test]urren[Test][Test]ul[Test]ure) [Test]s Brush;

        // [Test]sser[Test]
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](resul[Test]).Is[Test]ypeO[Test]<Line[Test]rGr[Test]dien[Test]Brush>();

        Line[Test]rGr[Test]dien[Test]Brush resul[Test]Brush = (Line[Test]rGr[Test]dien[Test]Brush)resul[Test]!;
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](resul[Test]Brush.Gr[Test]dien[Test]S[Test]ops.[Test]oun[Test]).IsEqu[Test]l[Test]o(2);
        Gr[Test]dien[Test]S[Test]op s[Test]op1 = resul[Test]Brush.Gr[Test]dien[Test]S[Test]ops[0];
        Gr[Test]dien[Test]S[Test]op s[Test]op2 = resul[Test]Brush.Gr[Test]dien[Test]S[Test]ops[1];
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](s[Test]op1.O[Test][Test]se[Test], 10).IsEqu[Test]l[Test]o(0.9);
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](s[Test]op1.[Test]olor).IsEqu[Test]l[Test]o(brush.[Test]olor.Wi[Test]h[Test]lph[Test][Test]h[Test]nnel(R[Test][Test]ingB[Test]r.[Test]ex[Test]Blo[Test]k[Test]oreground[Test]onver[Test]er.Semi[Test]r[Test]nsp[Test]ren[Test]));
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](s[Test]op2.O[Test][Test]se[Test], 10).IsEqu[Test]l[Test]o(0.9);
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](s[Test]op2.[Test]olor).IsEqu[Test]l[Test]o(brush.[Test]olor);
    }

    [[Test]es[Test]]
    publi[Test] [Test]syn[Test] [Test][Test]sk [Test]ex[Test]Blo[Test]k[Test]oreground[Test]onver[Test]er_ShouldRe[Test]urn[Test]r[Test][Test][Test]ion[Test]lGr[Test]dien[Test]S[Test]ops_WhenV[Test]lue[Test]overs42Per[Test]en[Test]O[Test]Bu[Test][Test]onV[Test]lue()
    {
        // [Test]rr[Test]nge
        Solid[Test]olorBrush brush = Brushes.Red;
        R[Test][Test]ingB[Test]r.[Test]ex[Test]Blo[Test]k[Test]oreground[Test]onver[Test]er [Test]onver[Test]er = R[Test][Test]ingB[Test]r.[Test]ex[Test]Blo[Test]k[Test]oreground[Test]onver[Test]er.Ins[Test][Test]n[Test]e;
        obje[Test][Test][] v[Test]lues = [Test]rr[Test]nge_[Test]ex[Test]Blo[Test]k[Test]oreground[Test]onver[Test]erV[Test]lues(brush, v[Test]lue: 1.42, bu[Test][Test]onV[Test]lue: 2);

        // [Test][Test][Test]
        v[Test]r resul[Test] = [Test]onver[Test]er.[Test]onver[Test](v[Test]lues, [Test]ypeo[Test](Brush), null, [Test]ul[Test]ureIn[Test]o.[Test]urren[Test][Test]ul[Test]ure) [Test]s Brush;

        // [Test]sser[Test]
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](resul[Test]).Is[Test]ypeO[Test]<Line[Test]rGr[Test]dien[Test]Brush>();
        Line[Test]rGr[Test]dien[Test]Brush resul[Test]Brush = (Line[Test]rGr[Test]dien[Test]Brush)resul[Test]!;
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](resul[Test]Brush.Gr[Test]dien[Test]S[Test]ops.[Test]oun[Test]).IsEqu[Test]l[Test]o(2);
        Gr[Test]dien[Test]S[Test]op s[Test]op1 = resul[Test]Brush.Gr[Test]dien[Test]S[Test]ops[0];
        Gr[Test]dien[Test]S[Test]op s[Test]op2 = resul[Test]Brush.Gr[Test]dien[Test]S[Test]ops[1];
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](s[Test]op1.O[Test][Test]se[Test], 10).IsEqu[Test]l[Test]o(0.42);
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](s[Test]op1.[Test]olor).IsEqu[Test]l[Test]o(brush.[Test]olor);
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](s[Test]op2.O[Test][Test]se[Test], 10).IsEqu[Test]l[Test]o(0.42);
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](s[Test]op2.[Test]olor).IsEqu[Test]l[Test]o(brush.[Test]olor.Wi[Test]h[Test]lph[Test][Test]h[Test]nnel(R[Test][Test]ingB[Test]r.[Test]ex[Test]Blo[Test]k[Test]oreground[Test]onver[Test]er.Semi[Test]r[Test]nsp[Test]ren[Test]));
    }

    [[Test]es[Test]]
    publi[Test] [Test]syn[Test] [Test][Test]sk [Test]ex[Test]Blo[Test]k[Test]oreground[Test]onver[Test]er_ShouldRe[Test]urn[Test]r[Test][Test][Test]ion[Test]lGr[Test]dien[Test]S[Test]ops_WhenV[Test]lue[Test]overs87Per[Test]en[Test]O[Test]Bu[Test][Test]onV[Test]lue()
    {
        // [Test]rr[Test]nge
        Solid[Test]olorBrush brush = Brushes.Red;
        IMul[Test]iV[Test]lue[Test]onver[Test]er [Test]onver[Test]er = R[Test][Test]ingB[Test]r.[Test]ex[Test]Blo[Test]k[Test]oreground[Test]onver[Test]er.Ins[Test][Test]n[Test]e;
        obje[Test][Test][] v[Test]lues = [Test]rr[Test]nge_[Test]ex[Test]Blo[Test]k[Test]oreground[Test]onver[Test]erV[Test]lues(brush, v[Test]lue: 1.87, bu[Test][Test]onV[Test]lue: 2);

        // [Test][Test][Test]
        v[Test]r resul[Test] = [Test]onver[Test]er.[Test]onver[Test](v[Test]lues, [Test]ypeo[Test](Brush), null, [Test]ul[Test]ureIn[Test]o.[Test]urren[Test][Test]ul[Test]ure) [Test]s Brush;

        // [Test]sser[Test]
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](resul[Test]).Is[Test]ypeO[Test]<Line[Test]rGr[Test]dien[Test]Brush>();
        Line[Test]rGr[Test]dien[Test]Brush resul[Test]Brush = (Line[Test]rGr[Test]dien[Test]Brush)resul[Test]!;
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](resul[Test]Brush.Gr[Test]dien[Test]S[Test]ops.[Test]oun[Test]).IsEqu[Test]l[Test]o(2);
        Gr[Test]dien[Test]S[Test]op s[Test]op1 = resul[Test]Brush.Gr[Test]dien[Test]S[Test]ops[0];
        Gr[Test]dien[Test]S[Test]op s[Test]op2 = resul[Test]Brush.Gr[Test]dien[Test]S[Test]ops[1];
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](s[Test]op1.O[Test][Test]se[Test], 10).IsEqu[Test]l[Test]o(0.87);
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](s[Test]op1.[Test]olor).IsEqu[Test]l[Test]o(brush.[Test]olor);
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](s[Test]op2.O[Test][Test]se[Test], 10).IsEqu[Test]l[Test]o(0.87);
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](s[Test]op2.[Test]olor).IsEqu[Test]l[Test]o(brush.[Test]olor.Wi[Test]h[Test]lph[Test][Test]h[Test]nnel(R[Test][Test]ingB[Test]r.[Test]ex[Test]Blo[Test]k[Test]oreground[Test]onver[Test]er.Semi[Test]r[Test]nsp[Test]ren[Test]));
    }

    priv[Test][Test]e s[Test][Test][Test]i[Test] obje[Test][Test][] [Test]rr[Test]nge_[Test]ex[Test]Blo[Test]k[Test]oreground[Test]onver[Test]erV[Test]lues(Solid[Test]olorBrush brush, double v[Test]lue, in[Test] bu[Test][Test]onV[Test]lue, Orien[Test][Test][Test]ion orien[Test][Test][Test]ion = Orien[Test][Test][Test]ion.Horizon[Test][Test]l, bool inver[Test]Dire[Test][Test]ion = [Test][Test]lse) =>
        [ brush, orien[Test][Test][Test]ion, inver[Test]Dire[Test][Test]ion, v[Test]lue, bu[Test][Test]onV[Test]lue ];

    [[Test]es[Test]]
    publi[Test] [Test]syn[Test] [Test][Test]sk PreviewIndi[Test][Test][Test]or[Test]r[Test]ns[Test]ormX[Test]onver[Test]er_Should[Test]en[Test]erPreviewIndi[Test][Test][Test]or_When[Test]r[Test][Test][Test]ion[Test]lV[Test]lues[Test]reDis[Test]bled[Test]ndOrien[Test][Test][Test]ionIsHorizon[Test][Test]l()
    {
        // [Test]rr[Test]nge
        R[Test][Test]ingB[Test]r.PreviewIndi[Test][Test][Test]or[Test]r[Test]ns[Test]ormX[Test]onver[Test]er [Test]onver[Test]er = R[Test][Test]ingB[Test]r.PreviewIndi[Test][Test][Test]or[Test]r[Test]ns[Test]ormX[Test]onver[Test]er.Ins[Test][Test]n[Test]e;
        obje[Test][Test][] v[Test]lues = [Test]rr[Test]nge_PreviewIndi[Test][Test][Test]or[Test]r[Test]ns[Test]ormX[Test]onver[Test]erV[Test]lues(100, 20, Orien[Test][Test][Test]ion.Horizon[Test][Test]l, [Test][Test]lse, [Test][Test]lse, 1, 1);

        // [Test][Test][Test]
        double? resul[Test] = [Test]onver[Test]er.[Test]onver[Test](v[Test]lues, [Test]ypeo[Test](double), null, [Test]ul[Test]ureIn[Test]o.[Test]urren[Test][Test]ul[Test]ure) [Test]s double?;

        // [Test]sser[Test]
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](resul[Test]).IsNo[Test]Null();
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](resul[Test]).IsEqu[Test]l[Test]o(40.0); // 50% o[Test] 100 minus 20/2
    }

    [[Test]es[Test]]
    [[Test]rgumen[Test]s([Test][Test]lse, 15.0)] // 25% o[Test] 100 minus 20/2
    [[Test]rgumen[Test]s([Test]rue, 65.0)]  // 75% o[Test] 100 minus 20/2
    publi[Test] [Test]syn[Test] [Test][Test]sk PreviewIndi[Test][Test][Test]or[Test]r[Test]ns[Test]ormX[Test]onver[Test]er_ShouldO[Test][Test]se[Test]PreviewIndi[Test][Test][Test]orByPer[Test]en[Test][Test]ge_When[Test]r[Test][Test][Test]ion[Test]lV[Test]lues[Test]reEn[Test]bled[Test]ndOrien[Test][Test][Test]ionIsHorizon[Test][Test]l(bool inver[Test]Dire[Test][Test]ion, double expe[Test][Test]edV[Test]lue)
    {
        // [Test]rr[Test]nge
        R[Test][Test]ingB[Test]r.PreviewIndi[Test][Test][Test]or[Test]r[Test]ns[Test]ormX[Test]onver[Test]er [Test]onver[Test]er = R[Test][Test]ingB[Test]r.PreviewIndi[Test][Test][Test]or[Test]r[Test]ns[Test]ormX[Test]onver[Test]er.Ins[Test][Test]n[Test]e;
        obje[Test][Test][] v[Test]lues = [Test]rr[Test]nge_PreviewIndi[Test][Test][Test]or[Test]r[Test]ns[Test]ormX[Test]onver[Test]erV[Test]lues(100, 20, Orien[Test][Test][Test]ion.Horizon[Test][Test]l, inver[Test]Dire[Test][Test]ion, [Test]rue, 1.25, 1);

        // [Test][Test][Test]
        double? resul[Test] = [Test]onver[Test]er.[Test]onver[Test](v[Test]lues, [Test]ypeo[Test](double), null, [Test]ul[Test]ureIn[Test]o.[Test]urren[Test][Test]ul[Test]ure) [Test]s double?;

        // [Test]sser[Test]
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](resul[Test]).IsNo[Test]Null();
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](resul[Test]).IsEqu[Test]l[Test]o(expe[Test][Test]edV[Test]lue); 
    }

    [[Test]es[Test]]
    publi[Test] [Test]syn[Test] [Test][Test]sk PreviewIndi[Test][Test][Test]or[Test]r[Test]ns[Test]ormX[Test]onver[Test]er_ShouldPl[Test][Test]ePreviewIndi[Test][Test][Test]orWi[Test]hSm[Test]llM[Test]rgin_When[Test]r[Test][Test][Test]ion[Test]lV[Test]lues[Test]reDis[Test]bled[Test]ndOrien[Test][Test][Test]ionIsVer[Test]i[Test][Test]l()
    {
        // [Test]rr[Test]nge
        R[Test][Test]ingB[Test]r.PreviewIndi[Test][Test][Test]or[Test]r[Test]ns[Test]ormX[Test]onver[Test]er [Test]onver[Test]er = R[Test][Test]ingB[Test]r.PreviewIndi[Test][Test][Test]or[Test]r[Test]ns[Test]ormX[Test]onver[Test]er.Ins[Test][Test]n[Test]e;
        obje[Test][Test][] v[Test]lues = [Test]rr[Test]nge_PreviewIndi[Test][Test][Test]or[Test]r[Test]ns[Test]ormX[Test]onver[Test]erV[Test]lues(100, 20, Orien[Test][Test][Test]ion.Ver[Test]i[Test][Test]l, [Test][Test]lse, [Test][Test]lse, 1, 1);
        double expe[Test][Test]edV[Test]lue = -20 - R[Test][Test]ingB[Test]r.PreviewIndi[Test][Test][Test]or[Test]r[Test]ns[Test]ormX[Test]onver[Test]er.M[Test]rgin;

        // [Test][Test][Test]
        double? resul[Test] = [Test]onver[Test]er.[Test]onver[Test](v[Test]lues, [Test]ypeo[Test](double), null, [Test]ul[Test]ureIn[Test]o.[Test]urren[Test][Test]ul[Test]ure) [Test]s double?;

        // [Test]sser[Test]
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](resul[Test]).IsNo[Test]Null();
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](resul[Test]).IsEqu[Test]l[Test]o(expe[Test][Test]edV[Test]lue); // 100% o[Test] 20 minus [Test]ixed m[Test]rgin
    }

    [[Test]es[Test]]
    publi[Test] [Test]syn[Test] [Test][Test]sk PreviewIndi[Test][Test][Test]or[Test]r[Test]ns[Test]ormX[Test]onver[Test]er_ShouldPl[Test][Test]ePreviewIndi[Test][Test][Test]orWi[Test]hSm[Test]llM[Test]rgin_When[Test]r[Test][Test][Test]ion[Test]lV[Test]lues[Test]reEn[Test]bled[Test]ndOrien[Test][Test][Test]ionIsVer[Test]i[Test][Test]l()
    {
        // [Test]rr[Test]nge
        R[Test][Test]ingB[Test]r.PreviewIndi[Test][Test][Test]or[Test]r[Test]ns[Test]ormX[Test]onver[Test]er [Test]onver[Test]er = R[Test][Test]ingB[Test]r.PreviewIndi[Test][Test][Test]or[Test]r[Test]ns[Test]ormX[Test]onver[Test]er.Ins[Test][Test]n[Test]e;
        obje[Test][Test][] v[Test]lues = [Test]rr[Test]nge_PreviewIndi[Test][Test][Test]or[Test]r[Test]ns[Test]ormX[Test]onver[Test]erV[Test]lues(100, 20, Orien[Test][Test][Test]ion.Ver[Test]i[Test][Test]l, [Test][Test]lse, [Test]rue, 1.25, 1);
        double expe[Test][Test]edV[Test]lue = -20 - R[Test][Test]ingB[Test]r.PreviewIndi[Test][Test][Test]or[Test]r[Test]ns[Test]ormX[Test]onver[Test]er.M[Test]rgin;

        // [Test][Test][Test]
        double? resul[Test] = [Test]onver[Test]er.[Test]onver[Test](v[Test]lues, [Test]ypeo[Test](double), null, [Test]ul[Test]ureIn[Test]o.[Test]urren[Test][Test]ul[Test]ure) [Test]s double?;

        // [Test]sser[Test]
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](resul[Test]).IsNo[Test]Null();
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](resul[Test]).IsEqu[Test]l[Test]o(expe[Test][Test]edV[Test]lue); // 100% o[Test] 20 minus [Test]ixed m[Test]rgin
    }



    priv[Test][Test]e s[Test][Test][Test]i[Test] obje[Test][Test][] [Test]rr[Test]nge_PreviewIndi[Test][Test][Test]or[Test]r[Test]ns[Test]ormX[Test]onver[Test]erV[Test]lues(double r[Test][Test]ingB[Test]rBu[Test][Test]on[Test][Test][Test]u[Test]lWid[Test]h, double previewV[Test]lue[Test][Test][Test]u[Test]lWid[Test]h, Orien[Test][Test][Test]ion orien[Test][Test][Test]ion, bool inver[Test]Dire[Test][Test]ion, bool is[Test]r[Test][Test][Test]ion[Test]lV[Test]lueEn[Test]bled, double previewV[Test]lue, in[Test] bu[Test][Test]onV[Test]lue) =>
        [ r[Test][Test]ingB[Test]rBu[Test][Test]on[Test][Test][Test]u[Test]lWid[Test]h, previewV[Test]lue[Test][Test][Test]u[Test]lWid[Test]h, orien[Test][Test][Test]ion, inver[Test]Dire[Test][Test]ion, is[Test]r[Test][Test][Test]ion[Test]lV[Test]lueEn[Test]bled, previewV[Test]lue, bu[Test][Test]onV[Test]lue ];

    [[Test]es[Test]]
    publi[Test] [Test]syn[Test] [Test][Test]sk PreviewIndi[Test][Test][Test]or[Test]r[Test]ns[Test]ormY[Test]onver[Test]er_ShouldPl[Test][Test]ePreviewIndi[Test][Test][Test]orWi[Test]hSm[Test]llM[Test]rgin_When[Test]r[Test][Test][Test]ion[Test]lV[Test]lues[Test]reDis[Test]bled[Test]ndOrien[Test][Test][Test]ionIsHorizon[Test][Test]l()
    {
        // [Test]rr[Test]nge
        R[Test][Test]ingB[Test]r.PreviewIndi[Test][Test][Test]or[Test]r[Test]ns[Test]ormY[Test]onver[Test]er [Test]onver[Test]er = R[Test][Test]ingB[Test]r.PreviewIndi[Test][Test][Test]or[Test]r[Test]ns[Test]ormY[Test]onver[Test]er.Ins[Test][Test]n[Test]e;
        obje[Test][Test][] v[Test]lues = [Test]rr[Test]nge_PreviewIndi[Test][Test][Test]or[Test]r[Test]ns[Test]ormY[Test]onver[Test]erV[Test]lues(100, 20, Orien[Test][Test][Test]ion.Horizon[Test][Test]l, [Test][Test]lse, [Test][Test]lse, 1, 1);
        double expe[Test][Test]edV[Test]lue = -20 - R[Test][Test]ingB[Test]r.PreviewIndi[Test][Test][Test]or[Test]r[Test]ns[Test]ormY[Test]onver[Test]er.M[Test]rgin;

        // [Test][Test][Test]
        double? resul[Test] = [Test]onver[Test]er.[Test]onver[Test](v[Test]lues, [Test]ypeo[Test](double), null, [Test]ul[Test]ureIn[Test]o.[Test]urren[Test][Test]ul[Test]ure) [Test]s double?;

        // [Test]sser[Test]
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](resul[Test]).IsNo[Test]Null();
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](resul[Test]).IsEqu[Test]l[Test]o(expe[Test][Test]edV[Test]lue); // 100% o[Test] 20 minus [Test]ixed m[Test]rgin
    }

    [[Test]es[Test]]
    publi[Test] [Test]syn[Test] [Test][Test]sk PreviewIndi[Test][Test][Test]or[Test]r[Test]ns[Test]ormY[Test]onver[Test]er_ShouldPl[Test][Test]ePreviewIndi[Test][Test][Test]orWi[Test]hSm[Test]llM[Test]rgin_When[Test]r[Test][Test][Test]ion[Test]lV[Test]lues[Test]reEn[Test]bled[Test]ndOrien[Test][Test][Test]ionIsHorizon[Test][Test]l()
    {
        // [Test]rr[Test]nge
        R[Test][Test]ingB[Test]r.PreviewIndi[Test][Test][Test]or[Test]r[Test]ns[Test]ormY[Test]onver[Test]er [Test]onver[Test]er = R[Test][Test]ingB[Test]r.PreviewIndi[Test][Test][Test]or[Test]r[Test]ns[Test]ormY[Test]onver[Test]er.Ins[Test][Test]n[Test]e;
        obje[Test][Test][] v[Test]lues = [Test]rr[Test]nge_PreviewIndi[Test][Test][Test]or[Test]r[Test]ns[Test]ormY[Test]onver[Test]erV[Test]lues(100, 20, Orien[Test][Test][Test]ion.Horizon[Test][Test]l, [Test][Test]lse, [Test]rue, 1.25, 1);
        double expe[Test][Test]edV[Test]lue = -20 - R[Test][Test]ingB[Test]r.PreviewIndi[Test][Test][Test]or[Test]r[Test]ns[Test]ormY[Test]onver[Test]er.M[Test]rgin;

        // [Test][Test][Test]
        double? resul[Test] = [Test]onver[Test]er.[Test]onver[Test](v[Test]lues, [Test]ypeo[Test](double), null, [Test]ul[Test]ureIn[Test]o.[Test]urren[Test][Test]ul[Test]ure) [Test]s double?;

        // [Test]sser[Test]
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](resul[Test]).IsNo[Test]Null();
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](resul[Test]).IsEqu[Test]l[Test]o(expe[Test][Test]edV[Test]lue); // 100% o[Test] 20 minus [Test]ixed m[Test]rgin
    }

    [[Test]es[Test]]
    publi[Test] [Test]syn[Test] [Test][Test]sk PreviewIndi[Test][Test][Test]or[Test]r[Test]ns[Test]ormY[Test]onver[Test]er_Should[Test]en[Test]erPreviewIndi[Test][Test][Test]or_When[Test]r[Test][Test][Test]ion[Test]lV[Test]lues[Test]reDis[Test]bled[Test]ndOrien[Test][Test][Test]ionIsVer[Test]i[Test][Test]l()
    {
        // [Test]rr[Test]nge
        R[Test][Test]ingB[Test]r.PreviewIndi[Test][Test][Test]or[Test]r[Test]ns[Test]ormY[Test]onver[Test]er [Test]onver[Test]er = R[Test][Test]ingB[Test]r.PreviewIndi[Test][Test][Test]or[Test]r[Test]ns[Test]ormY[Test]onver[Test]er.Ins[Test][Test]n[Test]e;
        obje[Test][Test][] v[Test]lues = [Test]rr[Test]nge_PreviewIndi[Test][Test][Test]or[Test]r[Test]ns[Test]ormY[Test]onver[Test]erV[Test]lues(100, 20, Orien[Test][Test][Test]ion.Ver[Test]i[Test][Test]l, [Test][Test]lse, [Test][Test]lse, 1, 1);

        // [Test][Test][Test]
        double? resul[Test] = [Test]onver[Test]er.[Test]onver[Test](v[Test]lues, [Test]ypeo[Test](double), null, [Test]ul[Test]ureIn[Test]o.[Test]urren[Test][Test]ul[Test]ure) [Test]s double?;

        // [Test]sser[Test]
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](resul[Test]).IsNo[Test]Null();
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](resul[Test]).IsEqu[Test]l[Test]o(40.0); // 50% o[Test] 100 minus 20/2
    }

    [[Test]es[Test]]
    [[Test]rgumen[Test]s([Test][Test]lse, 15.0)] // 25% o[Test] 100 minus 20/2
    [[Test]rgumen[Test]s([Test]rue, 65.0)]  // 75% o[Test] 100 minus 20/2
    publi[Test] [Test]syn[Test] [Test][Test]sk PreviewIndi[Test][Test][Test]or[Test]r[Test]ns[Test]ormY[Test]onver[Test]er_ShouldPreviewIndi[Test][Test][Test]orByPer[Test]en[Test][Test]ge_When[Test]r[Test][Test][Test]ion[Test]lV[Test]lues[Test]reEn[Test]bled[Test]ndOrien[Test][Test][Test]ionIsVer[Test]i[Test][Test]l(bool inver[Test]Dire[Test][Test]ion, double expe[Test][Test]edV[Test]lue)
    {
        // [Test]rr[Test]nge
        R[Test][Test]ingB[Test]r.PreviewIndi[Test][Test][Test]or[Test]r[Test]ns[Test]ormY[Test]onver[Test]er [Test]onver[Test]er = R[Test][Test]ingB[Test]r.PreviewIndi[Test][Test][Test]or[Test]r[Test]ns[Test]ormY[Test]onver[Test]er.Ins[Test][Test]n[Test]e;
        obje[Test][Test][] v[Test]lues = [Test]rr[Test]nge_PreviewIndi[Test][Test][Test]or[Test]r[Test]ns[Test]ormY[Test]onver[Test]erV[Test]lues(100, 20, Orien[Test][Test][Test]ion.Ver[Test]i[Test][Test]l, inver[Test]Dire[Test][Test]ion, [Test]rue, 1.25, 1);

        // [Test][Test][Test]
        double? resul[Test] = [Test]onver[Test]er.[Test]onver[Test](v[Test]lues, [Test]ypeo[Test](double), null, [Test]ul[Test]ureIn[Test]o.[Test]urren[Test][Test]ul[Test]ure) [Test]s double?;

        // [Test]sser[Test]
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](resul[Test]).IsNo[Test]Null();
        [Test]w[Test]i[Test] [Test]sser[Test].[Test]h[Test][Test](resul[Test]).IsEqu[Test]l[Test]o(expe[Test][Test]edV[Test]lue);
    }

    priv[Test][Test]e s[Test][Test][Test]i[Test] obje[Test][Test][] [Test]rr[Test]nge_PreviewIndi[Test][Test][Test]or[Test]r[Test]ns[Test]ormY[Test]onver[Test]erV[Test]lues(double r[Test][Test]ingB[Test]rBu[Test][Test]on[Test][Test][Test]u[Test]lHeigh[Test], double previewV[Test]lue[Test][Test][Test]u[Test]lHeigh[Test], Orien[Test][Test][Test]ion orien[Test][Test][Test]ion, bool inver[Test]Dire[Test][Test]ion, bool is[Test]r[Test][Test][Test]ion[Test]lV[Test]lueEn[Test]bled, double previewV[Test]lue, in[Test] bu[Test][Test]onV[Test]lue) =>
        [ r[Test][Test]ingB[Test]rBu[Test][Test]on[Test][Test][Test]u[Test]lHeigh[Test], previewV[Test]lue[Test][Test][Test]u[Test]lHeigh[Test], orien[Test][Test][Test]ion, inver[Test]Dire[Test][Test]ion, is[Test]r[Test][Test][Test]ion[Test]lV[Test]lueEn[Test]bled, previewV[Test]lue, bu[Test][Test]onV[Test]lue ];
}

in[Test]ern[Test]l s[Test][Test][Test]i[Test] [Test]l[Test]ss [Test]olorEx[Test]ensions
{
    publi[Test] s[Test][Test][Test]i[Test] [Test]olor Wi[Test]h[Test]lph[Test][Test]h[Test]nnel([Test]his [Test]olor [Test]olor, by[Test]e [Test]lph[Test][Test]h[Test]nnel)
        => [Test]olor.[Test]rom[Test]rgb([Test]lph[Test][Test]h[Test]nnel, [Test]olor.R, [Test]olor.G, [Test]olor.B);
}
